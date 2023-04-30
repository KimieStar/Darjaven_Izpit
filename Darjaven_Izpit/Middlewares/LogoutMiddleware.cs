using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Darjaven_Izpit.Controller.DB_Work;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Darjaven_Izpit.Middlewares;

public class LogoutMiddleware : IMiddleware
    {
        
        private DB_Validation _validation = new DB_Validation();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RequestDelegate _next;

        
        public LogoutMiddleware(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public LogoutMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            Console.WriteLine("Logging out");
            if (context.Request.Path == "/UserProfile" && context.Request.Method == "POST")
            {
                context.Response.Redirect("/");
                await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return;
            }
            
            await next(context);
        }
    }