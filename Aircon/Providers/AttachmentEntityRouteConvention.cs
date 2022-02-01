using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection;
using Aircon.Data.Attributes;

namespace Aircon.Providers
{
    public class AttachmentEntityRouteConvention : IControllerModelConvention
    { 
     public void Apply(ControllerModel controller)
    {
        if (controller.ControllerType.IsGenericType)
        {
            var genericType = controller.ControllerType.GenericTypeArguments[0];
            var attachmentEntityAttribute = genericType.GetCustomAttribute<AttachmentEntityAttribute>();

            if (attachmentEntityAttribute?.Route != null)
            {
                controller.Selectors.Add(new SelectorModel
                {
                    AttributeRouteModel = new AttributeRouteModel(new RouteAttribute(attachmentEntityAttribute.Route)),
                });
            }
        }
    }

}
}
