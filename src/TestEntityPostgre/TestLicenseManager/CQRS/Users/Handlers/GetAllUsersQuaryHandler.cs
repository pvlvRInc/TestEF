using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestLicenseManager.CQRS.Users.Handlers;
using TestLicenseManager.CQRS.Users.Handlers.Users.Queries;
using TestLicenseManager.Extensions;
using TestLicenseManager.Models;

namespace TestLicenseManager.CQRS.Users.Services;

public class GetAllUsersQuaryHandler : IQueryHandler<GetAllUserQuery>
{
    private readonly ApplicationDbContext _db;

    public GetAllUsersQuaryHandler(ApplicationDbContext db) =>
        _db = db;

    public async Task<IActionResult> HandleAsync(GetAllUserQuery query)
    {
        var list = await _db.Users.Select(x=>x.ToDTO()).ToListAsync();
        return new OkObjectResult(list);
    }

    public IActionResult Handle(GetAllUserQuery query)
    {
        throw new NotImplementedException();
    }
}