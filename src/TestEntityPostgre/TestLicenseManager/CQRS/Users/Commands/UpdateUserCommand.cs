using Library.DTOs;

namespace TestLicenseManager.CQRS.Users.Commands;

public class UpdateUserCommand : ICommand
{
    public int       Id        { get; init; }
    public UserModel UserModel { get; init; }
}