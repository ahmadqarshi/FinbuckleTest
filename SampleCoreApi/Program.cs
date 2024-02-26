using Microsoft.AspNetCore.Identity;
using SampleCoreApi.Context;
using Microsoft.EntityFrameworkCore;
using System;
using SampleCoreApi.Entities;
using Microsoft.Extensions.Configuration;
using Finbuckle.MultiTenant;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SchoolContext>((options =>
{
    
    options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=SchoolDb;Trusted_Connection=True;");
}));

builder.Services.AddIdentity<MyAppUser, MyAppRole>()
        .AddEntityFrameworkStores<SchoolContext>();

builder.Services.AddMultiTenant<TenantInfo>()
    .WithPerTenantAuthentication()
    .WithHostStrategy()
    .WithConfigurationStore();

builder.Services.AddIdentityCore<MyAppUser>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.Password.RequireDigit = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";



})
    .AddSignInManager<SignInManager<MyAppUser>>()
    .AddUserManager<UserManager<MyAppUser>>()
    .AddRoles<MyAppRole>()
    .AddEntityFrameworkStores<SchoolContext>()
    .AddDefaultTokenProviders();

var app = builder.Build();




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseMultiTenant();
//Enable CORS
app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
