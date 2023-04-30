using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Darjaven_Izpit.Middlewares;
using Darjaven_Izpit.Controller.DB_Work;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.DependencyInjection.Extensions;

DB_Connectivity_Check _connectivity = new DB_Connectivity_Check();
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<AuthMiddleware>();
builder.Services.AddScoped<LogoutMiddleware>();
builder.Services.AddScoped<RegisterMiddler>();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.Cookie.HttpOnly = true;
    options.SlidingExpiration = false;
});

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
app.UseAuthentication();

app.UseMiddleware<AuthMiddleware>();
app.UseMiddleware<LogoutMiddleware>();
app.UseMiddleware<RegisterMiddler>();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

if (_connectivity.isDbUp() == true)
{
    Console.WriteLine("DB State: ");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("DB is Open");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
}
else if(_connectivity.isDbUp() == false)
{
    Console.WriteLine("DB State: ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("DB is Closed");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
}

if (_connectivity.isConnectionOpen() == true)
{
    Console.WriteLine("");
    Console.WriteLine("Connection State: ");
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine("Connection is Open");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();
}

else if (_connectivity.isConnectionOpen() == false)
{
    Console.WriteLine("Connection State: ");
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine("Connection is Closed");
    Console.ForegroundColor = ConsoleColor.White;
    Console.WriteLine();

   
}

app.Run();