using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TestLicenseManager.Auth;
using TestLicenseManager.Models;

namespace TestLicenseManager.Controllers.Services;

public class AccountService
{
    private ApplicationDbContext _db;

    public AccountService(ApplicationDbContext db) =>
        _db = db;

    public IActionResult GetToken(string username, string password)
    {
        var identity = GetIdentity(username, password);
        if (identity == null)
        {
            return new BadRequestObjectResult(new { errorText = "Invalid username or password." });
        }

        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            AuthOptions.ISSUER,
            AuthOptions.AUDIENCE,
            notBefore: now,
            claims: identity.Claims,
            expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
        );
        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            username     = identity.Name
        };

        return new JsonResult(response);
    }

    private ClaimsIdentity GetIdentity(string email, string password)
    {
        User? person = _db.Users.FirstOrDefault(x => x.Email == email && x.HashPassword == password);
        if (person != null)
        {
            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, person.Email),
                new(ClaimsIdentity.DefaultRoleClaimType, person.Role.ToString())
            };
            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(
                    claims,
                    "Token",
                    ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType
                );
            return claimsIdentity;
        }

        return null;
    }
}