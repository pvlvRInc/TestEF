using Library.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace TestLicenseManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticatedTestController : ControllerBase
{
    [Authorize]
    [HttpGet("get_login")]
    public IActionResult GetName()
    {
        Log.Information("Getting name");
        return Ok($"Login: {User.Identity.Name}");
    }

    [Authorize(Roles = RoleConstants.Admin)]
    [HttpGet("get_role")]
    public IActionResult GetAll()
    {
        Log.Information("Getting role");
        return Ok($"Login: {User.Identity.Name} Role: Admin");
    }
}