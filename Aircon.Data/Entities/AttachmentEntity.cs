using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Aircon.Data.Entities
{
    public class AttachmentEntity
    {
        public int Id { get; set; }
        public int AttachmentTypeId { get; set; }
        public int AttachmentId { get; set; }

        [ForeignKey("AttachmentTypeId")]
        public AttachmentType AttachmentType { get; set; }
       
        [ForeignKey("AttachmentId")]
        public Attachment Attachment { get; set; }
    }
}
