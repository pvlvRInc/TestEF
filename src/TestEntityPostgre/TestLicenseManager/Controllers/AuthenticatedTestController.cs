using Library.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestLicenseManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticatedTestController : ControllerBase
{
    [Authorize]
    [HttpGet("get_login")]
    public IActionResult GetName() => 
        Ok($"Login: {User.Identity.Name}");
    
    [Authorize(Roles = RoleConstants.Admin)]
    [HttpGet("get_role")]
    public IActionResult GetAll() => 
        Ok($"Login: {User.Identity.Name} Role: Admin");
}