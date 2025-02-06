using Microsoft.EntityFrameworkCore;
using TestLicenseManager.CQRS.Users.Handlers;
using TestLicenseManager.CQRS.Users.Services;
using TestLicenseManager.Models;

namespace TestLicenseManager.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void RegisterAppDbContext(this WebApplicationBuilder webApplicationBuilder)
    {
        string connectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection");

        webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
    }

    public static void RegisterServices(this WebApplicationBuilder builder)
    {
        // Регистрация CommandService и QueryService
        builder.Services.AddScoped<UserCommandService>();
        builder.Services.AddScoped<UserQueryService>();

        // Регистрация хэндлеров команд
        builder.Services.AddTransient<CreateUserCommandHandler>();
        builder.Services.AddTransient<UpdateUserCommandHandler>();
        builder.Services.AddTransient<DeleteUserCommandHandler>();

        // Регистрация хэндлеров запросов
        builder.Services.AddTransient<GetUserQueryHandler>();
        builder.Services.AddTransient<GetAllUsersQuaryHandler>();
    }
}