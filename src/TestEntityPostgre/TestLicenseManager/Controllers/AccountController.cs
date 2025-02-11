using Microsoft.AspNetCore.Mvc;
using TestLicenseManager.Controllers.Services;

namespace TestLicenseManager.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AccountService _service;

    public AccountController(AccountService service) => 
        _service = service;

    [HttpPost("/token")]
    public IActionResult Token(string username, string password) => 
        _service.GetToken(username, password);
}