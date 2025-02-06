using Microsoft.AspNetCore.Mvc;
using TestLicenseManager.CQRS.Users.Handlers.Users.Queries;
using TestLicenseManager.Models;

namespace TestLicenseManager.CQRS.Users.Handlers;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery>
{
    private readonly ApplicationDbContext _db;

    public GetUserQueryHandler(ApplicationDbContext db) =>
        _db = db;

    public IActionResult Handle(GetUserQuery query)
    {
        User? user = _db.Users.FirstOrDefault(x => x.Id == query.Id);

        if (user is null)
            return new NotFoundResult();

        return new OkObjectResult(user);
    }

    public Task<IActionResult> HandleAsync(GetUserQuery query)
    {
        throw new NotImplementedException();
    }
}