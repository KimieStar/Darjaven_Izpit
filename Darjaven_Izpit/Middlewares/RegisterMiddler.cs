using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Darjaven_Izpit.Controller.DB_Work;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Darjaven_Izpit.Middlewares
{
    public class RegisterMiddler : IMiddleware
    {
        private DB_Validation _validation = new DB_Validation();
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly RequestDelegate _next;

        
        public RegisterMiddler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public RegisterMiddler(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path == "/Register" && context.Request.Method == "POST" )
            {
                DB_Create create = new DB_Create();
                
                string username = "";
                string password = "";
                string firstName = "";
                string lastName = "";
                string typeOfLogin = "";
                
                username = context.Request.Form["username"];
                password = context.Request.Form["password"];
                firstName = context.Request.Form["firstName"];
                lastName = context.Request.Form["lastName"];
                
                create.CreateUser(username,password,firstName,lastName);
                
                bool isAuthenticated = false;
                
                Console.WriteLine("Auth Initialized");
                if (_validation.ValidateUserAcc(username, password) != true)
                {
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.Redirect("/");
                     
                    return;
                }
                else if (_validation.ValidateUserAcc(username, password) == true)
                {
                    context.Response.StatusCode = StatusCodes.Status200OK;
                    context.Response.Redirect("/UserProfile");
                    isAuthenticated = true;
                }

                if (isAuthenticated == true)
                {

                    var claimsOfUser = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Role, "user"),
                        //new Claim(ClaimTypes.Email, email)
                    };
                     
                    var claimsOfGuest = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, "Guest"),
                        new Claim (ClaimTypes.Role, typeOfLogin),
                    };
                     
                    var identity = new ClaimsIdentity(claimsOfUser, "AuthCookie");
                    var principal = new ClaimsPrincipal(identity);
                    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    
                     
                    return;
                }
            }
            await next(context);
        }
    }
}