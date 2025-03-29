using E_Library.Models;
using E_Library.Services;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Swashbuckle.AspNetCore.SwaggerUI;
using E_Library.Services.Interface;
using Microsoft.AspNetCore.Identity;



// adding something to here
var builder = WebApplication.CreateBuilder(args);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
builder.WebHost.UseUrls($"http://*:{port}");

builder.Services.AddDbContext<ElibraryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddScoped<IAuthenticateServices, AuthenticateServices>();
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<ElibraryContext>()  // Add this line
    .AddDefaultTokenProviders();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {


        Title = "E-Library API",
        Version = "v1"
    });
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

//Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.MapScalarApiReference();
//app.MapOpenApi();
//}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "E-Library API V1");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
