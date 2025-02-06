namespace TestLicenseManager.CQRS.Users.Handlers.Users.Queries;

public class GetUserQuery : IQuery
{
    public int Id { get; set; }
}