using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Shared
{
    public class NotifyForgotPasswordModel
    {
        public string displayname { get; set; }
        public string link { get; set; }
        public string email { get; set; }
    }

    public class NotifyEmailModel
    {
        public string displayname { get; set; }
        public string email { get; set; }
    }

}
