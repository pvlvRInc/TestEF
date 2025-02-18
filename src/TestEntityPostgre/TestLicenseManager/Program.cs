using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using TestLicenseManager.Extensions;
using TestLicenseManager.Models;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()                                         // Минимальный уровень логирования
            .WriteTo.GrafanaLoki("http://localhost:3100", labels: new List<LokiLabel>() { new(){Key = "job", Value = "api"}})
            .WriteTo.Console()                                                  //Вывод логов в консоль
            .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day) // Запись в файл
            .CreateLogger();

// builder.Host.ConfigureLogging(logging =>
// {
//     logging.ClearProviders(); // Очищаем провайдеры логирования по умолчанию (например, Console)
//     logging.AddSerilog();     // Добавляем Serilog
//     // Можно добавить другие провайдеры логирования, если нужно
//     // logging.AddDebug();
// });


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

app.UseWebCustomSockets();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.MapIdentityApi<IdentityUser>();
app.UseAuthorization();

app.MapControllers();

app.Run();