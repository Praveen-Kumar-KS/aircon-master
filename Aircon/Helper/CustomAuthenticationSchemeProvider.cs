using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

namespace Aircon.Helper
{
    public class CustomAuthenticationSchemeProvider : AuthenticationSchemeProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public CustomAuthenticationSchemeProvider(
            IHttpContextAccessor httpContextAccessor,
            IOptions<AuthenticationOptions> options)
            : base(options)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        private async Task<AuthenticationScheme> GetRequestSchemeAsync()
        {
            var request = httpContextAccessor.HttpContext?.Request;
            if (request == null)
            {
                throw new ArgumentNullException("The HTTP request cannot be retrieved.");
            }

            // For API requests, use authentication tokens.
            if (request.Path.StartsWithSegments("/api"))
            {
                var result = await GetSchemeAsync(JwtBearerDefaults.AuthenticationScheme);
                return result;
            }

            // For the other requests, return null to let the base methods
            // decide what's the best scheme based on the default schemes
            // configured in the global authentication options.
            return null;
        }

        public override async Task<AuthenticationScheme> GetDefaultAuthenticateSchemeAsync() =>
            await GetRequestSchemeAsync() ??
            await base.GetDefaultAuthenticateSchemeAsync();

        public override async Task<AuthenticationScheme> GetDefaultChallengeSchemeAsync() =>
            await GetRequestSchemeAsync() ??
            await base.GetDefaultChallengeSchemeAsync();

        public override async Task<AuthenticationScheme> GetDefaultForbidSchemeAsync() =>
            await GetRequestSchemeAsync() ??
            await base.GetDefaultForbidSchemeAsync();

        public override async Task<AuthenticationScheme> GetDefaultSignInSchemeAsync() =>
            await GetRequestSchemeAsync() ??
            await base.GetDefaultSignInSchemeAsync();

        public override async Task<AuthenticationScheme> GetDefaultSignOutSchemeAsync() =>
            await GetRequestSchemeAsync() ??
            await base.GetDefaultSignOutSchemeAsync();
    }
}
