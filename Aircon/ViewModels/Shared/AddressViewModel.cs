using Aircon.Business.Models.Shared;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aircon.ViewModels.Shared
{
    public class AddressViewModel
    {
        [Display(Name = "Nick Name")]
        public string NickName { get; set; }
        [Required]
        [Display(Name = "Address Line1")]
        public string Line1 { get; set; }
        [Display(Name = "Address Line2")]
        public string Line2 { get; set; }
        [Required]
        [Display(Name = "City")]
        [RegularExpression("^[a-zA-Z\\s]+$", ErrorMessage ="Enter a valid city Name")]
        public string City { get; set; }
        [Display(Name = "State")]
        [Required]
        public string State { get; set; }
        [RegularExpression(@"^\d{5}(?:[-\s]\d{4})?$",
        ErrorMessage = "Please enter a Valid Zip Code")]
        [Display(Name = "Zip")]
        [Required]
        public string Zip { get; set; }
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }
        public int Id { get; set; }
        public string GetFullAddress ()
        {
            return string.Format("{0} {1} {2} {3} {4} {5} ", NickName, Line1, Line2, City, State, Zip);
        }
    }
}

