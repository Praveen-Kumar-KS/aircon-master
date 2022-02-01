using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Models.Identity.SignUp
{
    public class SignUpCompanyProfileModel
    {
        public int Id { get; set; }
        public string CompanyName { get; set; }
        public string FranchiseParent { get; set; }
        public string AdminEmail { get; set; }
        public string AlternateEmail { get; set; }
        public string IATANumber { get; set; }
        public string EinOrSsn { get; set; }
    }
}
