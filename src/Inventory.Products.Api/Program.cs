
using Inventory.Products.Api.Worker;
using Inventory.Products.Business.Services;
using Inventory.Products.Business.Services.IServices;
using Inventory.Products.Business.Utils;
using Inventory.Products.Business.Utils.IUilts;
using Inventory.Products.DataAccess.Helpers;
using Inventory.Products.DataAccess.Helpers.IHelpers;
using Inventory.Products.DataAccess.Models.Configurations;
using Inventory.Products.DataAccess.Models.Entites;
using Inventory.Products.DataAccess.Repositories;
using Inventory.Products.DataAccess.Repositories.IRepositories;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuration
IConfiguration Configuration = builder.Configuration;
Configuration["LogicConfiguration:DatabaseConnection"] = Environment.GetEnvironmentVariable("DatabaseConnection");
Configuration["LogicConfiguration:SecretKey"] = Environment.GetEnvironmentVariable("SecretKey");
Configuration["LogicConfiguration:UserPool"] = Environment.GetEnvironmentVariable("UserPool");
Configuration["LogicConfiguration:ClientId"] = Environment.GetEnvironmentVariable("ClientId");
Configuration["LogicConfiguration:QueueBulk"] = Environment.GetEnvironmentVariable("QueueBulk");
builder.Services.Configure<LogicConfiguration>(builder.Configuration.GetSection("LogicConfiguration"));

builder.Services.AddSingleton<IAuthenticationServices, AuthenticationServices>();
builder.Services.AddSingleton<ICatalogsServices, CatalogsServices>();
builder.Services.AddSingleton<IProductServices, ProductServices>();
builder.Services.AddSingleton<IUsersServices, UsersServices>();
builder.Services.AddSingleton<ISqsPublisher, SqsPublisher>();
builder.Services.AddSingleton<IAuthenticationRepository, AuthenticationRepository>();
builder.Services.AddSingleton<ICatalogsRepository, CatalogsRepository>();
builder.Services.AddSingleton<IProductsRepository, ProductsRepository>();
builder.Services.AddSingleton<IUsersRepository, UsersRepository>();
builder.Services.AddSingleton<IDapperHelper, DapperHelper>();
builder.Services.AddHostedService<ProductWorker>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(x =>
{
    x.AllowAnyHeader();
    x.AllowAnyMethod();
    x.AllowAnyOrigin();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Middleware de validación JWT y Basic Authentication
app.Use(async (context, next) =>
{
    string urlAuthentication = context.Request.Path;
    if (urlAuthentication.Contains("Authentication"))
    {
        await next.Invoke();
    }
    else
    {
        //var token = context.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", "");
        //if (!string.IsNullOrEmpty(token))
        //{
        //    var secretKey = Environment.GetEnvironmentVariable("SecretKey");
        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        //    try
        //    {
        //        var handler = new JwtSecurityTokenHandler();
        //        handler.ValidateToken(token, new TokenValidationParameters
        //        {
        //            ValidateIssuerSigningKey = true,
        //            IssuerSigningKey = key,
        //            ValidateIssuer = false,
        //            ValidateAudience = false,
        //            ClockSkew = TimeSpan.Zero
        //        }, out _);
        //    }
        //    catch (Exception ex)
        //    {
        //        context.Response.StatusCode = 401;
        //        await context.Response.WriteAsync("Invalid token");
        //        return;
        //    }
        //}
        //else
        //{
        //    context.Response.StatusCode = 401;
        //    await context.Response.WriteAsync("Missing token");
        //    return;
        //}
        await next.Invoke();
    }

});

app.Run();
