using Aircon.Business.Models.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Aircon.ViewModels.Shared
{
    public class NoteViewModel
    {
        public int NoteId { get; set; }
        [Display(Name = "Text")]
        [Required]
        public string Text { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }
    }

    public class NoteListViewModel
    {
        public int EntityId { get; set; }
        public List<NoteViewModel> Notes { get; set; }

        public NoteListViewModel()
        {
            Notes = new List<NoteViewModel>();
        }
    }

    public class NoteEntityViewModel
    {
        public int EntityId { get; set; }
        public string EntityName { get; set; }

        public int NoteId { get; set; }
        [Display(Name = "Say something..")]
        [Required]
        public string Text { get; set; }
        public int CreatedById { get; set; }
        public DateTime CreatedOn { get; set; }

    }


}
