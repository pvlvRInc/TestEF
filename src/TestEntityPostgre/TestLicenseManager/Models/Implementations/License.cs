using Library.DTOs.Abstraction;

namespace TestLicenseManager.Models;

public class License : ModelBase
{
    public string Key           { get; set; }
    public string Version       { get; set; }
    public int    MaxUsersCount { get; set; }

    public List<User> Users { get; set; }

    public int     CompanyId { get; set; }
    public Company Company   { get; set; }
}