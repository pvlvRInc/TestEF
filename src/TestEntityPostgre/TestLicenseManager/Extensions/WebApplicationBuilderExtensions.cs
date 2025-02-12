using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TestLicenseManager.Controllers.Services;
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
        builder.Services.AddScoped<AccountService>();

        // Регистрация хэндлеров команд
        builder.Services.AddTransient<CreateUserCommandHandler>();
        builder.Services.AddTransient<UpdateUserCommandHandler>();
        builder.Services.AddTransient<DeleteUserCommandHandler>();

        // Регистрация хэндлеров запросов
        builder.Services.AddTransient<GetUserQueryHandler>();
        builder.Services.AddTransient<GetAllUsersQuaryHandler>();
    }
    
    public static void AddSwaggerDocWithAuth(this WebApplicationBuilder builder)
    {
        builder.Services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1",
                new OpenApiInfo
                {
                    Title          = "Demo License Manager",
                    Description    = null,
                    Version        = "v1",
                    TermsOfService = null,
                    Contact        = null,
                    License        = null,
                    Extensions     = null
                });
        
            options.AddSecurityDefinition("Bearer",
                new OpenApiSecurityScheme
                {
                    Type                = SecuritySchemeType.Http,
                    Description         = "Enter JWT Bearer token",
                    Name                = "Authorization",
                    In                  = ParameterLocation.Header,
                    Scheme              = "bearer",
                    BearerFormat        = "JWT",
                    Flows               = null,
                    OpenIdConnectUrl    = null,
                    Extensions          = null,
                    UnresolvedReference = false,
                    Reference           = null
                });
        
            options.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference           = new OpenApiReference
                        {
                            ExternalResource = null,
                            Type             = ReferenceType.SecurityScheme,
                            Id               = "Bearer"
                        }
                    },
                    []
                }
            });
        });
    }
}