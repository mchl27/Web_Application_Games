using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Application_web_Arquitecture.Models;

var builder = WebApplication.CreateBuilder(args);

// Add the configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

// Configure services
builder.Services.AddDbContext<DbWebApplicationContext>((serviceProvider, options) =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
