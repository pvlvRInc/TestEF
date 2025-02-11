using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TestLicenseManager.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<User>    Users     { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<License> Licenses  { get; set; }
    public DbSet<Plugin>  Plugins   { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}
