using webApi.Services.Repository.Impelement;
using webApi.Services.Repository.Interface;
using webApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

// Add services to the container.
services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add Data Base Context
services.AddDbContext<SqlServerContext>(options => options.UseSqlServer("Data Source=VAHID\\MSSQLSERVER01; Initial Catalog = webApiDB; Integrated Security = True;Encrypt = True;TrustServerCertificate = True;User Instance = False"));
// Add Scopes
services.AddScoped<IUserRepository, UserRepository>();
services.AddScoped<IWalletRialRepository, WalleRialRepository>();
services.AddScoped<IWalletDollerRepository, WalletDollerRepository>();
services.AddScoped<IPaymentRepository, PaymentRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
