using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Darjaven_Izpit.Controller.DB_Work;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Darjaven_Izpit.Middlewares
{
    public class AuthMiddleware : IMiddleware
    {
        private DB_Validation validation = new DB_Validation();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RequestDelegate _next;

        
        public AuthMiddleware(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path == "/" && context.Request.Method == "POST" )
            {
                string username = "";
                string password = "";
                string typeOfLogin = "";
                
                    username = context.Request.Form["username"];
                    password = context.Request.Form["password"];
                    typeOfLogin = context.Request.Form["typeOfLogin"];
                
                
                
                bool isAuthenticated = false;

                if (typeOfLogin == "user")
                {
                    Console.WriteLine("Auth Initialized");
                    if (validation.ValidateUserAcc(username, password) != true)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.Redirect("/");
                     
                        return;
                    }
                    else if (validation.ValidateUserAcc(username, password) == true)
                    {
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        context.Response.Redirect("/UserProfile");
                        isAuthenticated = true;
                    }
                }
                else if (typeOfLogin == "admin")
                {
                    Console.WriteLine("Auth Initialized");
                    if (validation.ValidateAdminAcc(username, password) != true)
                    {
                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.Redirect("/");
                     
                        return;
                    }
                    else if (validation.ValidateAdminAcc(username, password) == true)
                    {
                        context.Response.StatusCode = StatusCodes.Status200OK;
                        context.Response.Redirect("/UserProfile");
                        isAuthenticated = true;
                    }
                }
                

                if (isAuthenticated == true)
                {

                    if (typeOfLogin == "user")
                    {
                        var claimsOfUser = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, username),
                            new Claim(ClaimTypes.Role, "user"),
                            //new Claim(ClaimTypes.Email, email)
                        };
                        
                        var identity = new ClaimsIdentity(claimsOfUser, "AuthCookie");
                        var principal = new ClaimsPrincipal(identity);
                        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    }
                    else if (typeOfLogin == "admin")
                    {
                        var claimsOfAdmin = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, username),
                            new Claim (ClaimTypes.Role, "admin"),
                        };
                        
                        var identity = new ClaimsIdentity(claimsOfAdmin, "AuthCookie");
                        var principal = new ClaimsPrincipal(identity);
                        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    }
                     
                    
                    
                     
                    return;
                }
            }
            await next(context);
        }
    }
}