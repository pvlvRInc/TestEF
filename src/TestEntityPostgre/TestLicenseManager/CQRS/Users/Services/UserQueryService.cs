using Microsoft.AspNetCore.Mvc;
using TestLicenseManager.CQRS.Users.Handlers;
using TestLicenseManager.CQRS.Users.Handlers.Users.Queries;

namespace TestLicenseManager.CQRS.Users.Services;

public class UserQueryService
{
    private readonly GetUserQueryHandler     _getSingleHandler;
    private readonly GetAllUsersQuaryHandler _getAllUsersHandler;

    public UserQueryService(GetUserQueryHandler getSingleHandler, GetAllUsersQuaryHandler getAllUsersHandler)
    {
        _getSingleHandler        = getSingleHandler;
        _getAllUsersHandler = getAllUsersHandler;
    }

    public IActionResult GetUser(GetUserQuery query) =>
        _getSingleHandler.Handle(query);

    public Task<IActionResult> GetAllUsersAsync(GetAllUserQuery query) =>
        _getAllUsersHandler.HandleAsync(query);
}