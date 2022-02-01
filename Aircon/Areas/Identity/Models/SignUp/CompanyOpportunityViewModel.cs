using Aircon.ViewModels.Shared;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Identity.Models.SignUp
{
    public class CompanyOpportunityViewModel
    {
        [Display(Name = "Company Name")]
        public string CompanyName { get; set; }
        public string FranchiseParent { get; set; }
        public string AdminEmail { get; set; }
        public string AlternateEmail { get; set; }
        public string IATANumber { get; set; }
        public string EINNumber { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public List<AddressViewModel> Addresses { get; set; }
        public string NoOfBranches { get; set; }
        public string Plan { get; set; }
        public bool IsAccepted { get; set; }

        public CompanyOpportunityViewModel()
        {
            Addresses = new List<AddressViewModel>();
        }
    }
}
