using Aircon.Business.Services.Shared;
using Aircon.ViewModels.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Components
{
    [ViewComponent(Name = "Note")]
    public class NoteViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync(int entityId, string entityName)
        {
            var model = new NoteEntityViewModel { EntityId = entityId, EntityName = entityName  };
            return View(model);
        }
    }
}
