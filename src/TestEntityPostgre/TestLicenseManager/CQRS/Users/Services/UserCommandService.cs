using Microsoft.AspNetCore.Mvc;
using TestLicenseManager.CQRS.Users.Commands;
using TestLicenseManager.CQRS.Users.Handlers;

namespace TestLicenseManager.CQRS.Users.Services;

public class UserCommandService
{
    private readonly CreateUserCommandHandler _createHandler;
    private readonly UpdateUserCommandHandler _updateHandler;
    private readonly DeleteUserCommandHandler _deleteHandler;

    public UserCommandService(
        CreateUserCommandHandler createHandler,
        UpdateUserCommandHandler updateHandler,
        DeleteUserCommandHandler deleteHandler
    )
    {
        _createHandler = createHandler;
        _updateHandler = updateHandler;
        _deleteHandler = deleteHandler;
    }

    public IActionResult CreateUser(CreateUserCommand command) =>
        _createHandler.Handle(command);

    public IActionResult UpdateUser(UpdateUserCommand command) =>
        _updateHandler.Handle(command);

    public IActionResult DeleteUser(DeleteUserCommand command) =>
        _deleteHandler.Handle(command);
}