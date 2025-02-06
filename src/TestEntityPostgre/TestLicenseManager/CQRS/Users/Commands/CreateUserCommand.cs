using Library.DTOs;

namespace TestLicenseManager.CQRS.Users.Commands;

public class CreateUserCommand : ICommand
{
    public UserModel UserModel { get; init; }
}