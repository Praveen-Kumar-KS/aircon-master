using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace Aircon.Business.Models.Shared
{
    public class AttachmentModel
    {
        public int Id { get; set; }
        public List<SelectListItem> AttachmentTypes { get; set; }
        public List<AttachmentListModel> Attachments { get; set; }

    }
    public class AttachmentListModel
    {
        public int AttachmentId { get; set; }
        public int AttachmentTypeId { get; set; }
        public string AttachmentTypeName { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public string MimeType { get; set; }
        public long Size { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
