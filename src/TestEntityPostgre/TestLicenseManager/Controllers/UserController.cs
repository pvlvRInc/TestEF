using Library.DTOs;
using Microsoft.AspNetCore.Mvc;
using TestLicenseManager.Extensions;
using TestLicenseManager.Models;

namespace TestLicenseManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private ApplicationDbContext _db;

    public UserController(ApplicationDbContext db) =>
        _db = db;

    [HttpPost("create")]
    public IActionResult CreateUser([FromBody] UserModel userModel)
    {
        if (userModel is not null)
        {
            _db.Users.Add(userModel.FromDTO());
            _db.SaveChanges();
            return Ok();
        }

        return BadRequest();
    }
}