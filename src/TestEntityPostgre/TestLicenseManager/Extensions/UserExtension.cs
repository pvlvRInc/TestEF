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
}