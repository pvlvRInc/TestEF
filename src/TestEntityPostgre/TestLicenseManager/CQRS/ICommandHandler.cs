using Microsoft.AspNetCore.Mvc;
using TestLicenseManager.CQRS.Users.Commands;

namespace TestLicenseManager.CQRS.Users.Handlers;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    IActionResult Handle(TCommand command);
}