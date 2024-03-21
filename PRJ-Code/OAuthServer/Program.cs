using IdentityServer4.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using OAuthServer.Data;
using OAuthServer.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Users")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

var configurationString = builder.Configuration.GetConnectionString("Configuration");

builder.Services.AddIdentityServer(options =>
    {
        options.Events.RaiseErrorEvents = true;
        options.Events.RaiseInformationEvents = true;
        options.Events.RaiseFailureEvents = true;
        options.Events.RaiseSuccessEvents = true;

        options.UserInteraction = new UserInteractionOptions
        {
            LoginUrl = "/Account/Login",
            LogoutUrl = "/Account/Logout",
            ConsentUrl = "/Consent/Index",
            ErrorUrl = "/Home/Error",
        };
    })
    .AddAspNetIdentity<ApplicationUser>()
    .AddConfigurationStore(options =>
    {
        options.ConfigureDbContext = db => db.UseSqlite(configurationString);
    })
    .AddOperationalStore(options =>
    {
        options.ConfigureDbContext = db =>
        db.UseSqlite(configurationString);

        // this enables automatic token cleanup. this is optional.
        //options.EnableTokenCleanup = true;
        // options.TokenCleanupInterval = 15; // interval in seconds. 15 seconds useful for debugging
    }); ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
