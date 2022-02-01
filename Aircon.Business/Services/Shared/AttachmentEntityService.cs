using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Aircon.Business.Extensions;
using Aircon.Business.Models.Shared;
using Aircon.Data;
using Aircon.Data.Entities;

namespace Aircon.Business.Services.Shared
{
    public interface IAttachmentEntityService<T>
    {
        List<AttachmentListModel> GetAll(int Id);
        AttachmentListModel Add(int id,AttachmentListModel attachmentModel);
        AttachmentListModel GetById(int Id, int AttachmentId);
        AttachmentListModel Update(int Id,AttachmentListModel attachmentModel);
    }
    public class AttachmentEntityService<T> : IAttachmentEntityService<T> where T : AttachmentEntity
    {
        private readonly AirconDbContext _airconDBContext;
        public AttachmentEntityService(AirconDbContext airconDbContext)
        {
            _airconDBContext = airconDbContext;
        }
        public List<AttachmentListModel> GetAll(int Id)
        {
            return _airconDBContext.Set<T>().AsNoTracking().Where(x => x.Id == Id).Select(x => new AttachmentListModel
            {
                AttachmentTypeId = x.AttachmentTypeId,
                AttachmentId=x.AttachmentId,
                Description = x.Attachment.Description,
                CreatedBy = x.Attachment.CreatedBy,
                CreatedOnUtc = x.Attachment.CreatedOnUtc,
                Location = x.Attachment.Location,
                MimeType = x.Attachment.MimeType,
                Name = x.Attachment.Name,
                Size = x.Attachment.Size,
                AttachmentTypeName = x.AttachmentType.Description
            }).ToList();
        }
        public AttachmentListModel Add(int id, AttachmentListModel attachmentModel)
        {
            var attachment = attachmentModel.GetAttachmentEntity<T>();
            attachment.Id = id;
            attachment.AttachmentTypeId = attachmentModel.AttachmentTypeId;
            attachment.AttachmentId = attachmentModel.AttachmentId;
            _airconDBContext.Set<T>().Add(attachment);
            _airconDBContext.SaveChanges();
            attachmentModel.AttachmentId = attachment.Attachment.Id;
            return attachmentModel;
        }
        public AttachmentListModel GetById(int Id, int AttachmentId)
        {
            return _airconDBContext.Set<T>().AsNoTracking().Where(x => x.Id == Id && x.AttachmentId == AttachmentId).Select(x => new AttachmentListModel
            {
                AttachmentTypeId = x.AttachmentTypeId,
                AttachmentId = x.AttachmentId,
                Description = x.Attachment.Description,
                CreatedBy = x.Attachment.CreatedBy,
                CreatedOnUtc = x.Attachment.CreatedOnUtc,
                Location = x.Attachment.Location,
                MimeType = x.Attachment.MimeType,
                Name = x.Attachment.Name,
                Size = x.Attachment.Size,
                AttachmentTypeName = x.AttachmentType.Description
            }).SingleOrDefault();
        }
        public AttachmentListModel Update(int id, AttachmentListModel attachmentModel)
        {
            var attachment = _airconDBContext.Attachments.Find(attachmentModel.AttachmentId);
            attachment.Description = attachmentModel.Description;
            _airconDBContext.Attachments.Update(attachment);
            _airconDBContext.SaveChanges();
            return attachmentModel;
        }
        

    }
}

