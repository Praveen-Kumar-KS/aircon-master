using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Test
{
    class TestUserFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            context.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, UserSettings.UserId),
            new Claim(ClaimTypes.Name, UserSettings.Name),
            new Claim(ClaimTypes.Email, UserSettings.UserEmail),
            new Claim(ClaimTypes.Role, "Admin")
        }));

            await next();
        }
    }
}
