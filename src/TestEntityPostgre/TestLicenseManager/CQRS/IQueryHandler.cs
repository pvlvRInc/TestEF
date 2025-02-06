using Microsoft.AspNetCore.Mvc;

namespace TestLicenseManager.CQRS.Users.Handlers;

public interface IQueryHandler<TQuery>
{
    IActionResult Handle(TQuery query);
    Task<IActionResult> HandleAsync(TQuery query);
}