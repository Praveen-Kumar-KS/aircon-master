using Aircon.Data;
using Vg.Common.Notification;
using Vg.Common.Notification.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Services
{
    public class TemplateDefinitionService : ITemplateDefinitionService
    {
        private readonly AirconDbContext _airconDbContext;


        public TemplateDefinitionService(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }
        public TemplateDefinitionModel Get(string name)
        {
            return _airconDbContext.TemplateDefinitions.Where(x => x.Name == name).Select(x=>
                new TemplateDefinitionModel
                {
                    Id = x.Id,
                    TemplateText = x.TemplateText,
                    Name = x.TemplateText,
                    SampleTemplateText = x.SampleTemplateText,
                    Layout = x.Layout,
                    IsLayout = x.IsLayout,
                    Instructions = x.Instructions,
                    DisplayName = x.DisplayName,
                    EmailSubjectText = x.EmailSubjectText
                }
            ).SingleOrDefault();
        }

        public IReadOnlyList<TemplateDefinitionModel> GetAll()
        {
            return _airconDbContext.TemplateDefinitions.Select(x =>
                new TemplateDefinitionModel
                {
                    Id = x.Id,
                    TemplateText = x.TemplateText,
                    Name = x.TemplateText,
                    SampleTemplateText = x.SampleTemplateText,
                    Layout = x.Layout,
                    IsLayout = x.IsLayout,
                    Instructions = x.Instructions,
                    DisplayName = x.DisplayName,
                    EmailSubjectText = x.EmailSubjectText
                }
            ).ToList();
        }

        public TemplateDefinitionModel GetOrNull(string name)
        {
            return _airconDbContext.TemplateDefinitions.Where(x => x.Name == name).Select(x =>
                new TemplateDefinitionModel
                {
                    Id = x.Id,
                    TemplateText = x.TemplateText,
                    Name = x.TemplateText,
                    SampleTemplateText = x.SampleTemplateText,
                    Layout = x.Layout,
                    IsLayout = x.IsLayout,
                    Instructions = x.Instructions,
                    DisplayName = x.DisplayName,
                    EmailSubjectText = x.EmailSubjectText
                }
            ).SingleOrDefault();
        }
    }

}
