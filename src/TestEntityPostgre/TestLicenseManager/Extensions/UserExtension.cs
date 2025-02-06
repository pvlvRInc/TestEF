using Library.DTOs;
using TestLicenseManager.Models;

namespace TestLicenseManager.Extensions;

public static class UserExtension
{
    public static UserModel ToDTO(this User user) =>
        new()
        {
            Id           = user.Id,
            CreatedAt    = user.CreatedAt,
            UpdatedAt    = user.UpdatedAt,
            FirstName    = user.FirstName,
            MiddleName   = user.MiddleName,
            LastName     = user.LastName,
            Email        = user.Email,
            HashPassword = user.HashPassword
        };

    public static User FromDTO(this UserModel user) =>
        new()
        {
            CreatedAt    = DateTime.UtcNow,
            UpdatedAt    = DateTime.UtcNow,
            FirstName    = user.FirstName,
            MiddleName   = user.MiddleName,
            LastName     = user.LastName,
            Email        = user.Email,
            HashPassword = user.HashPassword
        };

    public static User UpdateFromDTO(this User user, UserModel model)
    {
        user.UpdatedAt    = DateTime.UtcNow;
        user.FirstName    = model.FirstName;
        user.MiddleName   = model.MiddleName;
        user.LastName     = model.LastName;
        user.Email        = model.Email;
        user.HashPassword = model.HashPassword;
        return user;
    }
}