using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
//using Kendo.Mvc.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Aircon.Business.Models.Shared;
using Aircon.Business.Services;
using Aircon.Business.Services.Shared;
using Aircon.Data.Entities;
using Aircon.Extensions;
using Aircon.FileUpload.Service;
using Aircon.ViewModels.Shared;

namespace Aircon.Controllers.Shared
{
    [Authorize(Roles = "Partners")]
    public class AttachmentEntityBaseController<T> : Controller where T : AttachmentEntity
    {
        private readonly IAttachmentEntityService<T> AttachmentEntityService;
        private readonly IStoredFileService StoredFileService;
        private readonly IAzureStorageService AzureStorageService;
        public AttachmentEntityBaseController(IAttachmentEntityService<T> attachmentEntityService, IStoredFileService storedFileService, IAzureStorageService azureStorageService)
        {
            AttachmentEntityService = attachmentEntityService;
            StoredFileService = storedFileService;
            AzureStorageService = azureStorageService;
        }
        [HttpGet("TabGrid/{Id}")]
        public IActionResult TabGrid(int Id)
        {
            ViewData["AttachmentGridController"] = typeof(T).Name;
            AttachmentViewModel attachmentViewModel = new AttachmentViewModel();
            attachmentViewModel.Id = Id;
            attachmentViewModel.AttachmentTypes = LookupHelper.GetAllLookupItems<AttachmentType>();
            attachmentViewModel.Attachments = new List<AttachmentListViewModel>();
            attachmentViewModel.Attachments = AttachmentEntityService.GetAll(Id).Select(x => new AttachmentListViewModel
            {
                AttachmentTypeId = x.AttachmentTypeId,
                AttachmentTypeName = x.AttachmentTypeName,
                AttachmentId = x.AttachmentId,
                Description = x.Description,
                MimeType = x.MimeType,
                Location = x.Location,
                Name = x.Name,
                Size = x.Size,
                CreatedBy = x.CreatedBy,
                CreatedOn = x.CreatedOnUtc
            }).ToList();
            foreach (var item in attachmentViewModel.Attachments)
            {
                FileInfo fileInFo = new FileInfo(item.Name);
                string ext = fileInFo.Extension;
                string fileName = string.Format("{0}{1}", StoredFileService.GetDirectoryPath(item.AttachmentId), ext);
                item.DisplayUrl = AzureStorageService.GetAttachmentFromStorage(fileName);
            }
            return PartialView("~/Views/AttachmentEntity/Index.cshtml", attachmentViewModel);
        }
        [HttpPost("Create")]
        public async Task<ActionResult> Create(int id, int attachmentTypeId, string description, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                StoredFileModel attachment = new StoredFileModel();
                if (imageFile != null && imageFile.Length > 0)
                {
                    attachment = StoredFileService.CreateTempStoredFile();
                    attachment.Name = System.IO.Path.GetFileName(imageFile.FileName);
                    attachment.MimeType = imageFile.ContentType;
                    attachment.Size = imageFile.Length;
                    attachment.Description = description;
                    attachment = StoredFileService.SaveStoredFile(attachment);
                    FileInfo fileInFo = new FileInfo(imageFile.FileName);
                    string ext = fileInFo.Extension;
                    string fileName = string.Format("{0}{1}", StoredFileService.GetDirectoryPath(attachment.Id), ext);
                    await AzureStorageService.UploadAttachmentToStorage(imageFile.OpenReadStream(), fileName);
                }
                if (attachment.Id != 0)
                {
                    AttachmentEntityService.Add(id, new AttachmentListModel
                    {
                        AttachmentTypeId = attachmentTypeId,
                        AttachmentId = attachment.Id
                    });
                }

            }
            string trimString = typeof(T).Name.Replace("Attachment", string.Empty);
            return RedirectToAction("View" + trimString, trimString + "s", new { id = id });
        }

        [HttpPost("Update")]
        public async Task<ActionResult> Update(int id, int attachmentId, string editdescription, IFormFile imageFile)
        {
            if (ModelState.IsValid)
            {
                StoredFileModel attachment = StoredFileService.GetStoredFileById(attachmentId);
                if (imageFile != null && imageFile.Length > 0)
                {
                    attachment.Name = System.IO.Path.GetFileName(imageFile.FileName);
                    attachment.MimeType = imageFile.ContentType;
                    attachment.Size = imageFile.Length;
                    attachment = StoredFileService.SaveStoredFile(attachment);
                    FileInfo fileInFo = new FileInfo(imageFile.FileName);
                    string ext = fileInFo.Extension;
                    string fileName = string.Format("{0}{1}", StoredFileService.GetDirectoryPath(attachment.Id), ext);
                    await AzureStorageService.UploadAttachmentToStorage(imageFile.OpenReadStream(), fileName);
                }
                AttachmentEntityService.Update(id, new AttachmentListModel
                {
                    AttachmentId = attachment.Id,
                    Description = editdescription
                });
            }
            string trimString = typeof(T).Name.Replace("Attachment", string.Empty);
            return RedirectToAction("View" + trimString, trimString + "s", new { id = id });
        }

        [HttpGet("DownloadAttachment")]
        public async Task<ActionResult> DownloadAttachment(int id, int attachmentId)
        {
            AttachmentListModel attachmentViewModel = new AttachmentListModel();
            attachmentViewModel = AttachmentEntityService.GetById(id, attachmentId);
            FileInfo fileInFo = new FileInfo(attachmentViewModel.Name);
            string ext = fileInFo.Extension;
            string fileName = string.Format("{0}{1}", StoredFileService.GetDirectoryPath(attachmentViewModel.AttachmentId), ext);
            if (AzureStorageService.HasAttachment(fileName).Result)
            {
                Stream blobStream = await AzureStorageService.DownloadAttachmentFromStorage(fileName);
                return File(blobStream, attachmentViewModel.MimeType, attachmentViewModel.Name);
            }
            else
            {
                return Content("Attachment does not exist");
            }

        }


    }
}