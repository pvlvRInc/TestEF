using Microsoft.EntityFrameworkCore;
using TestLicenseManager.Models;

namespace TestLicenseManager.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void RegisterAppDbContext(this WebApplicationBuilder webApplicationBuilder)
    {
        string connectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection");

        webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    }
}