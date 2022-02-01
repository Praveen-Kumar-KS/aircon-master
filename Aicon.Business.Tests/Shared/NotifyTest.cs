using Aircon.Business.Models.Shared;
using Aircon.Core.Data;
using Vg.Common.Notification.Message;
using Aircon.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Aicon.Business.UnitTests.Shared
{
    [Collection("AirconWebApplicationFactory")]
    public class NotifyTest : IntegrationTestBase , IDisposable
    {
        private readonly INotify _notify;
        public NotifyTest(AirconWebApplicationFactory factory) : base(factory)
        {
            _notify = GetRequiredService<INotify>();
        }


        [Fact]
        public void NotifyEmailShouldSend()
        {
            var notifyModel = new NotifyForgotPasswordModel { displayname = string.Format("{0} {1}", "FirstName", "LastName"), link = string.Empty };
            _notify.NotifyAsync("Test@gmail.com", TemplateDefinitionNames.General.PasswordReset, notifyModel);

        }
        public void Dispose()
        {

        }

    }
}
