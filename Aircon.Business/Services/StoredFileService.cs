using System;
using System.IO;
using System.Linq;
using Aircon.Business.Models.Shared;
using Aircon.Data;
using Aircon.Data.Entities;

namespace Aircon.Business.Services
{
    public interface IStoredFileService
    {
        StoredFileModel SaveStoredFile(StoredFileModel storedFileModel);
        StoredFileModel GetStoredFileById(int storedFileId);
        StoredFileModel CreateTempStoredFile();
        string GetDirectoryPath(int storedFileId);
        string GetWebUriPath(int storedFileId);
    }

    public class StoredFileService : IStoredFileService
    {
        private readonly AirconDbContext _airconDBContext;
        public StoredFileModel storedFileModel { get; set; }

        private const string mediaPathFormat = "{0}/{1}/{2}/{3}";


        public StoredFileService(AirconDbContext airconDbContext)
        {
            _airconDBContext = airconDbContext;
        }

        public StoredFileModel CreateTempStoredFile()
        {
            Attachment attachment = new Attachment();
            attachment.Name = string.Empty;
            attachment.Location = string.Empty;
            attachment.MimeType = string.Empty;
            attachment.Size = 0;
            _airconDBContext.Attachments.Add(attachment);
            _airconDBContext.SaveChanges();
            string physicalLocation = string.Format("{0}/{1}/{2}", Math.Abs(attachment.Id / 1000000).ToString("D2"), Math.Abs((attachment.Id / 10000) % 100).ToString("D2"), Math.Abs((attachment.Id / 100) % 100).ToString("D2"));
            attachment.Location = physicalLocation;
            _airconDBContext.Attachments.Update(attachment);
            _airconDBContext.SaveChanges();
            StoredFileModel sfm = new StoredFileModel { Id = attachment.Id, Location = attachment.Location };
            return sfm;
        }

        public StoredFileModel SaveStoredFile(StoredFileModel storedFileModel)
        {
            Attachment attachment = _airconDBContext.Attachments.Find(storedFileModel.Id);
            attachment.Location = storedFileModel.Location;
            attachment.MimeType = storedFileModel.MimeType;
            attachment.Name = storedFileModel.Name;
            attachment.Size = storedFileModel.Size;
            attachment.Description = storedFileModel.Description;
            _airconDBContext.Attachments.Update(attachment);
            _airconDBContext.SaveChanges();
            storedFileModel.Id = attachment.Id;
            return storedFileModel;
        }

        public StoredFileModel GetStoredFileById(int storedFileId)
        {
            StoredFileModel storedFile = (from sf in _airconDBContext.Attachments.AsQueryable()
                                              where sf.Id == storedFileId
                                              select new StoredFileModel
                                              {
                                                  Id = sf.Id,
                                                  Name = sf.Name,
                                                  MimeType = sf.MimeType,
                                                  Location = sf.Location,
                                                  Size = sf.Size
                                              }).SingleOrDefault();
            return storedFile;
        }

        public string GetDirectoryPath(int storedFileId)
        {
            string path = Path.Combine(Math.Abs(storedFileId / 1000000).ToString("D2"), Math.Abs((storedFileId / 10000) % 100).ToString("D2"), Math.Abs((storedFileId / 100) % 100).ToString("D2"), storedFileId.ToString("D8"));
            return path;
        }

        public string GetWebUriPath(int storedFileId)
        {
            return string.Format(mediaPathFormat, Math.Abs(storedFileId / 1000000).ToString("D2"), Math.Abs((storedFileId / 10000) % 100).ToString("D2"), Math.Abs((storedFileId / 100) % 100).ToString("D2"), storedFileId.ToString("D8"));
        }
    }
}
