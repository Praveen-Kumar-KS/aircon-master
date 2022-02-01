using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.Collections.Generic;
using System.Linq;
using Aircon.Data.Attributes;
using System.Reflection;
using Aircon.Controllers.Shared;

namespace Aircon.Providers
{
    public class AttachmentEntityControllerProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            var currentAssembly = typeof(AttachmentEntityAttribute).Assembly;
            var candidates = currentAssembly.GetExportedTypes().Where(x => x.GetCustomAttributes<AttachmentEntityAttribute>().Any());
            foreach (var candidate in candidates)
            {
                feature.Controllers.Add(
                    typeof(AttachmentEntityBaseController<>).MakeGenericType(candidate).GetTypeInfo()
                );
            }


        }
    }
}
