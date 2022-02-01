using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Aircon.Data.Entities
{
    public class PreferenceEntity 
    {
        public int Id { get; set; }
        public int PreferenceId { get; set; }
        [ForeignKey("PreferenceId")]
        public Preference Preference { get; set; }
    }
}
