namespace TestLicenseManager.CQRS.Users.Commands;

public class DeleteUserCommand : ICommand
{
    public int Id { get; init; }
}