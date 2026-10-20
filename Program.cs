using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SchoolApi.Application.Interfaces.IServices;
using SchoolApi.Application.Interfaces.IUnitOfWork;
using SchoolApi.Application.Services;
using SchoolApi.Domain.Entities;
using SchoolApi.Infrastructure.Persistence.Data;
using SchoolApi.Infrastructure.Persistence.Services;
using SchoolApi.Infrastructure.Persistence.UnitOfWork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
       builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IStudentService, StudentServices>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IAuthService, AuthServices>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var JwtSettings = builder.Configuration.GetSection("jwt");
var secretKey = JwtSettings["key"];

if (string.IsNullOrWhiteSpace(secretKey))
{
    throw new InvalidOperationException("Jwt secret key is empty or missing");
};

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidIssuer = JwtSettings["Issuer"],
        ValidAudience = JwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});



builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
