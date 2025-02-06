using Microsoft.AspNetCore.Mvc;
using TestLicenseManager.CQRS.Users.Commands;
using TestLicenseManager.Models;

namespace TestLicenseManager.CQRS.Users.Handlers;

public class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly ApplicationDbContext _db;

    public DeleteUserCommandHandler(ApplicationDbContext db) =>
        _db = db;

    public IActionResult Handle(DeleteUserCommand command)
    {
        User? user = _db.Users.FirstOrDefault(x => x.Id == command.Id);

        if (user is null) 
            return new NotFoundResult();
        
        _db.Users.Remove(user);
        _db.SaveChanges();
        
        return new OkResult();

    }
}