using Library.DTOs.Abstraction;

namespace TestLicenseManager.Models;

public class Company : ModelBase
{
    public string Name  { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public List<User> Users { get; set; }

    public License License { get; set; }
}