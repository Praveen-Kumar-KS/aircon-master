using Aircon.Business.Services.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Threading.Tasks;

namespace Aircon.TagHelpers
{
    [HtmlTargetElement(Attributes = "asp-authorize")]
    [HtmlTargetElement(Attributes = "asp-authorize,asp-permission")]
    public class AuthorizeTagHelper : TagHelper
    {
        private readonly IPermissionService _permissionService;

        public AuthorizeTagHelper(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        /// <summary>
        /// Gets or sets the policy name that determines access to the HTML block.
        /// </summary>
        [HtmlAttributeName("asp-permission")]
        public string Permission { get; set; }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (!await _permissionService.Authorize(Permission))
            {
                output.SuppressOutput();
            }

            if (output.Attributes.TryGetAttribute("asp-authorize", out TagHelperAttribute attribute))
                output.Attributes.Remove(attribute);
        }
    }
}
