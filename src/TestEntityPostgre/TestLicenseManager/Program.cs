using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using TestLicenseManager.Extensions;
using TestLicenseManager.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddAuthorization();

builder.Services
       .AddIdentityApiEndpoints<IdentityUser>()
       .AddEntityFrameworkStores<ApplicationDbContext>();

builder.AddSwaggerDocWithAuth();
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

app.UseWebCustomSockets();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();