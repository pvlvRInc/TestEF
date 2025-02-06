using Microsoft.AspNetCore.Mvc;
using TestLicenseManager.CQRS.Users.Commands;
using TestLicenseManager.Extensions;
using TestLicenseManager.Models;

namespace TestLicenseManager.CQRS.Users.Handlers;

public class CreateUserCommandHandler : ICommandHandler<CreateUserCommand>
{
    private readonly ApplicationDbContext _db;

    public CreateUserCommandHandler(ApplicationDbContext db) =>
        _db = db;

    public IActionResult Handle(CreateUserCommand command)
    {
        if (command.UserModel is null)
            return new BadRequestResult();

        _db.Users.Add(command.UserModel.FromDTO());
        _db.SaveChanges();

        return new OkResult();
    }
}