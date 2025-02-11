using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using TestLicenseManager.Extensions;
using TestLicenseManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//        .AddJwtBearer(options =>
//         {
//             options.RequireHttpsMetadata = false;
//             options.TokenValidationParameters = new TokenValidationParameters
//             {
//                 // укзывает, будет ли валидироваться издатель при валидации токена
//                 ValidateIssuer = true,
//                 // строка, представляющая издателя
//                 ValidIssuer = AuthOptions.ISSUER,
//
//                 // будет ли валидироваться потребитель токена
//                 ValidateAudience = true,
//                 // установка потребителя токена
//                 ValidAudience = AuthOptions.AUDIENCE,
//                 // будет ли валидироваться время существования
//                 ValidateLifetime = true,
//
//                 // установка ключа безопасности
//                 IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
//                 // валидация ключа безопасности
//                 ValidateIssuerSigningKey = true,
//             };
//         });

builder.Services.AddAuthorization();
builder.Services
       .AddIdentityApiEndpoints<IdentityUser>()
       .AddEntityFrameworkStores<ApplicationDbContext>();

builder.RegisterAppDbContext();
builder.RegisterServices();

var app = builder.Build();

app.MapIdentityApi<IdentityUser>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseAuthentication();
// app.UseAuthorization();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();