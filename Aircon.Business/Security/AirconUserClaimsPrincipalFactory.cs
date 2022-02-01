using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Aircon.Data.Entities;
using System.Security.Claims;
using System.Threading.Tasks;
using Aircon.Core.Security;

namespace Aircon.Business.Security
{
    public class AirconUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
    {
        public AirconUserClaimsPrincipalFactory(
            UserManager<User> userManager,
            RoleManager<Role> roleManager,
            IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        {
        }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(User user)
        {
            var identity = await base.GenerateClaimsAsync(user);
            identity.AddClaim(new Claim(ClaimTypes.Email, user.Email ?? ""));
            identity.AddClaim(new Claim(AirconClaimType.UserId, user.Id.ToString()));
            identity.AddClaim(new Claim(AirconClaimType.CustomerId, user.CustomerId.HasValue ? user.CustomerId.Value.ToString() : string.Empty));
            identity.AddClaim(new Claim(AirconClaimType.FullName, string.Format("{0} {1}",user.FirstName,user.LastName)));
            return identity;
        }
    }
}
