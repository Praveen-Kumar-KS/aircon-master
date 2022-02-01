using System;

namespace Aircon.ViewModels.Shared
{
    public class StoredFileViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string MimeType { get; set; }
        public long Size { get; set; }
        public bool Active { get; set; }
    }
}
