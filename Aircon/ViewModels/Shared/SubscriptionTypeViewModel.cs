using System.ComponentModel.DataAnnotations;

namespace Aircon.ViewModels.Shared
{
    public class SubscriptionTypeViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Monthly Amount")]
        public int MonthlyAmount { get; set; }
        [Display(Name = "Annual Amount")]
        public int AnnualAmount { get; set; }
        [Display(Name = "IsPopular")]
        public bool IsPopular { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Active")]
        public bool Active { get; set; }
        [Display(Name = "Line1")]
        public string Line1 { get; set; }
        [Display(Name = "Line2")]
        public string Line2 { get; set; }
        [Display(Name = "Line3")]
        public string Line3 { get; set; }
        [Display(Name = "Line4")]
        public string Line4 { get; set; }
        [Display(Name = "Display Order")]
        public int DisplayOrder { get; set; }
    }
}
