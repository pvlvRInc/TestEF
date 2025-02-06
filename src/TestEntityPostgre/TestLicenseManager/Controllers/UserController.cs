using Library.DTOs;
using Microsoft.AspNetCore.Mvc;
using TestLicenseManager.CQRS.Users.Commands;
using TestLicenseManager.CQRS.Users.Handlers.Users.Queries;
using TestLicenseManager.CQRS.Users.Services;

namespace TestLicenseManager.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserCommandService _commandService;
    private readonly UserQueryService   _queryService;

    public UserController(UserCommandService commandService, UserQueryService queryService)
    {
        _commandService = commandService;
        _queryService   = queryService;
    }

    [HttpPost("create")]
    public IActionResult CreateUser([FromBody] UserModel userModel) =>
        _commandService.CreateUser(new CreateUserCommand
        {
            UserModel = userModel
        });

    [HttpPatch("update/{id}")]
    public IActionResult UpdateUser(int id, [FromBody] UserModel userModel) =>
        _commandService.UpdateUser(new UpdateUserCommand
        {
            Id        = id,
            UserModel = userModel
        });

    [HttpDelete("delete/{id}")]
    public IActionResult DeleteUser(int id) =>
        _commandService.DeleteUser(new DeleteUserCommand
        {
            Id = id
        });
    
    [HttpGet("get/{id}")]
    public IActionResult GetUser(int id) =>
        _queryService.GetUser(new GetUserQuery
        {
            Id = id
        });
    
    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers(int id) =>
        await _queryService.GetAllUsersAsync(new GetAllUserQuery());
}