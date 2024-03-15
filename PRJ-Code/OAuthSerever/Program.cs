using Microsoft.EntityFrameworkCore;
using OAuthSerever.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

//Pøipojení databáze
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite($"Filename={Path.Combine(Path.GetTempPath(), "openid-server.sqlite3")}");
    options.UseOpenIddict();
});

builder.Services.AddOpenIddict().AddCore(options =>
{
    options.UseEntityFrameworkCore().UseDbContext<AppDbContext>();
}).AddServer(options =>
{
    options.SetTokenEndpointUris("connect/token");
    options.AllowClientCredentialsFlow();
    options.AddDevelopmentEncryptionCertificate().AddDevelopmentSigningCertificate();
    options.UseAspNetCore().EnableTokenEndpointPassthrough();
});

//Pøipojení identity

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
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapDefaultControllerRoute();
});

app.MapRazorPages();

app.Run();
