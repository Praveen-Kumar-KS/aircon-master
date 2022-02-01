using System.ComponentModel.DataAnnotations;

namespace Aircon.Areas.Customer.Models.Preference
{
    public class TemplateDefinitionViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Max Name Length")]
        public int MaxNameLength { get; set; }
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "Template Text")]
        public string TemplateText { get; set; }
        [Display(Name = "Is Layout")]
        public bool IsLayout { get; }
        [Display(Name = "Layout")]
        public string Layout { get; set; }
        [Display(Name = "Sample Template Text")]
        public string SampleTemplateText { get; set; }
        [Display(Name = "Instructions")]
        public string Instructions { get; set; }
    }
}
