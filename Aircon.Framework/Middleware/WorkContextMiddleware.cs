using Aircon.Core;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Framework.Middleware
{
    public class WorkContextMiddleware
    {
        private readonly RequestDelegate _next;

        public WorkContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IWorkContext workContext)
        {
            if (context == null || context.Request == null)
            {
                await _next(context);
                return;
            }
            var user = await workContext.SetCurrentUser();
            await _next(context);
        }
    }
}
