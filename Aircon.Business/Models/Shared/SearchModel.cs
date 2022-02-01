using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aircon.Business.Models.Shared
{
    public class SearchModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Display(Name = "ActiveId")]
        public string ActiveCode { get; set; }
        [Display(Name = "ActiveName")]
        public string ActiveName { get; set; }
        [Display(Name = "ResultsPerPageId")]
        public string ResultsPerPageCode { get; set; }
        [Display(Name = "ResultsPerPageName")]
        public string ResultsPerPageName { get; set; }
        public List<SelectListItem> ActiveList { get; set; }
        public List<SelectListItem> ResultsPerPageList { get; set; }
    }
}
