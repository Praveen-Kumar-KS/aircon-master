//using Microsoft.AspNetCore.Mvc.ApplicationParts;
//using Microsoft.AspNetCore.Mvc.Controllers;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using Aircon.Controllers.Shared;
//using Aircon.Data.Attributes;

//namespace Aircon.Providers
//{
//    public class LookupControllerProvider : IApplicationFeatureProvider<ControllerFeature>
//    {
//        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
//        {
//            var currentAssembly = typeof(LookupAttribute).Assembly;
//            var candidates = currentAssembly.GetExportedTypes().Where(x => x.GetCustomAttributes<LookupAttribute>().Any());
//            foreach (var candidate in candidates)
//            {
//                feature.Controllers.Add(
//                    typeof(LookupBaseController<>).MakeGenericType(candidate).GetTypeInfo()
//                );
//            }
//        }
//    }
//}
