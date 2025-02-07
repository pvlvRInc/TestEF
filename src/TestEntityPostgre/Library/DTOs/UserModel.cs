using Library.DTOs.Abstraction;
using Library.Enums;

namespace Library.DTOs;

public class UserModel : ModelBase
{
    public string FirstName    { get; set; }
    public string MiddleName   { get; set; }
    public string LastName     { get; set; }
    public string Email        { get; set; }
    public string HashPassword { get; set; }
    public ERole  Role         { get; set; }
}