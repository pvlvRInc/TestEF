using Library.DTOs.Abstraction;
using Library.Enums;

namespace TestLicenseManager.Models;

public class User : ModelBase
{
    public string FirstName    { get; set; }
    public string MiddleName   { get; set; }
    public string LastName     { get; set; }
    public string Email        { get; set; }
    public string HashPassword { get; set; }
    public ERole  Role         { get; set; }

    public int?    CompanyId { get; set; }
    public Company Company   { get; set; }

    public int?    LicenseId { get; set; }
    public License License   { get; set; }
}