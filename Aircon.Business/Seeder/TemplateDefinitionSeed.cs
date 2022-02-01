using Aircon.Business.Extensions;
using Aircon.Core.Data;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Data.Enums;
using Aircon.Data.Security;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Seeder
{
    public class TemplateDefinitionSeed : BaseDataSeeder
    {
        public override string SeedType => SeedDataType.System;

        public override int Order => 2;

        private readonly AirconDbContext _airconDbContext;

        public TemplateDefinitionSeed(AirconDbContext airconDbContext)
        {
            _airconDbContext = airconDbContext;
        }

        public override async Task SeedAsync()
        {
            var emailLayout = _airconDbContext.TemplateDefinitions.Where(x => x.Name == TemplateDefinitionNames.Layout.EmailLayout).SingleOrDefault();
            if (emailLayout == null)
            {
                _airconDbContext.TemplateDefinitions.Add(TemplateDefinationProvider.EmailLayout);
                _airconDbContext.TemplateDefinitions.Add(TemplateDefinationProvider.GeneralPasswordReset);
                await _airconDbContext.SaveChangesAsync();
            }
            var generalSignUpWelcomeEmail = _airconDbContext.TemplateDefinitions.Where(x => x.Name == TemplateDefinitionNames.General.SignUpWelcomeEmail).SingleOrDefault();
            if (generalSignUpWelcomeEmail == null)
            {
                _airconDbContext.TemplateDefinitions.Add(TemplateDefinationProvider.GeneralSignUpWelcomeEmail);
                await _airconDbContext.SaveChangesAsync();
            }
            var approveConfirmEmail = _airconDbContext.TemplateDefinitions.Where(x => x.Name == TemplateDefinitionNames.General.ApprovingUserEmail).SingleOrDefault();
            if (approveConfirmEmail == null)
            {
                _airconDbContext.TemplateDefinitions.Add(TemplateDefinationProvider.ApproveUserEmail);
                await _airconDbContext.SaveChangesAsync();
            }

            var approveCustomerEmail = _airconDbContext.TemplateDefinitions.Where(x => x.Name == TemplateDefinitionNames.General.ApprovingCustomerEmail).SingleOrDefault();
            if (approveCustomerEmail == null)
            {
                _airconDbContext.TemplateDefinitions.Add(TemplateDefinationProvider.ApproveCustomerEmail);
                await _airconDbContext.SaveChangesAsync();
            }
            var activateConfirmEmail = _airconDbContext.TemplateDefinitions.Where(x => x.Name == TemplateDefinitionNames.General.ActivatingUserEmail).SingleOrDefault();
            if (activateConfirmEmail == null)
            {
                _airconDbContext.TemplateDefinitions.Add(TemplateDefinationProvider.ActivatingUserEmail);
                await _airconDbContext.SaveChangesAsync();
            }

            var activateCustomerEmail = _airconDbContext.TemplateDefinitions.Where(x => x.Name == TemplateDefinitionNames.General.ActivatingCustomerEmail).SingleOrDefault();
            if (activateCustomerEmail == null)
            {
                _airconDbContext.TemplateDefinitions.Add(TemplateDefinationProvider.ActivatingCustomerEmail);
                await _airconDbContext.SaveChangesAsync();
            }

            var emailAtaQuote = _airconDbContext.TemplateDefinitions.Where(x => x.Name == TemplateDefinitionNames.Quotes.AtaQuote).SingleOrDefault();
            if (emailAtaQuote == null)
            {
                _airconDbContext.TemplateDefinitions.Add(TemplateDefinationProvider.EmailAtaQuote);
                await _airconDbContext.SaveChangesAsync();
            }

            var emailDtaQuote = _airconDbContext.TemplateDefinitions.Where(x => x.Name == TemplateDefinitionNames.Quotes.DtaQuote).SingleOrDefault();
            if (emailDtaQuote == null)
            {
                _airconDbContext.TemplateDefinitions.Add(TemplateDefinationProvider.EmailDtaQuote);
                await _airconDbContext.SaveChangesAsync();
            }

        }
    }

}
