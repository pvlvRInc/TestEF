using System.Runtime.CompilerServices;
using Library.DTOs.Abstraction;

namespace TestLicenseManager.Models;

public class User : ModelBase
{
    public string FirstName    { get; set; }
    public string MiddleName   { get; set; }
    public string LastName     { get; set; }
    public string Email        { get; set; }
    public string HashPassword { get; set; }

    public int?      CompanyId { get; set; }
    public Company Company   { get; set; }

    public int? LicenseId { get; set; }
    public License License   { get; set; }
}