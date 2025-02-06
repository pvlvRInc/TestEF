using Microsoft.AspNetCore.Mvc;
using TestLicenseManager.CQRS.Users.Commands;
using TestLicenseManager.Extensions;
using TestLicenseManager.Models;

namespace TestLicenseManager.CQRS.Users.Handlers;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand>
{
    private readonly ApplicationDbContext _db;

    public UpdateUserCommandHandler(ApplicationDbContext db) =>
        _db = db;

    public IActionResult Handle(UpdateUserCommand command)
    {
        if (command.UserModel is null)
            return new BadRequestResult();

        User? user = _db.Users.FirstOrDefault(x => x.Id == command.Id);

        if (user is null)
            return new NotFoundResult();

        _db.Users.Update(user.UpdateFromDTO(command.UserModel));
        _db.SaveChanges();

        return new OkResult();
    }
}