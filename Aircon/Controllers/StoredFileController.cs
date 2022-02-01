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

namespace Aircon.Controllers
{
    public class StoredFileController : Controller
    {

        private readonly IStoredFileService StoredFileService;
        private readonly IAzureStorageService AzureStorageService;
        
        
        public StoredFileController( IStoredFileService storedFileService, IAzureStorageService azureStorageService)
        {
            StoredFileService = storedFileService;
            AzureStorageService = azureStorageService;
        }
        public IActionResult Index()
        {
            return View();
        }

        //[HttpPost("Create")]
        //public async Task<ActionResult> Create(int id, int attachmentTypeId, string description, IFormFile imageFile)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        StoredFileModel attachment = new StoredFileModel();
        //        if (imageFile != null && imageFile.Length > 0)
        //        {
        //            attachment = StoredFileService.CreateTempStoredFile();
        //            attachment.Name = System.IO.Path.GetFileName(imageFile.FileName);
        //            attachment.MimeType = imageFile.ContentType;
        //            attachment.Size = imageFile.Length;
        //            attachment.Description = description;
        //            attachment = StoredFileService.SaveStoredFile(attachment);
        //            FileInfo fileInFo = new FileInfo(imageFile.FileName);
        //            string ext = fileInFo.Extension;
        //            string fileName = string.Format("{0}{1}", StoredFileService.GetDirectoryPath(attachment.Id), ext);
        //            await AzureStorageService.UploadAttachmentToStorage(imageFile.OpenReadStream(), fileName);
        //        }

        //    }
        //    string trimString = typeof(T).Name.Replace("Attachment", string.Empty);
        //    return RedirectToAction("View" + trimString, trimString + "s", new { id = id });
        //}

        [HttpPost]
        //do not validate request token (XSRF)
        [IgnoreAntiforgeryToken]
        public virtual async Task<IActionResult> AsyncUpload()
        {
            //if (!await _permissionService.Authorize(StandardPermissionProvider.UploadPictures))
            //    return Json(new { success = false, error = "You do not have required permissions" }, "text/plain");

            var httpPostedFile = Request.Form.Files.FirstOrDefault();
            if (httpPostedFile == null)
                return Json(new { success = false, message = "No file uploaded" });

            const string qqFileNameParameter = "qqfilename";

            var qqFileName = Request.Form.ContainsKey(qqFileNameParameter)
                ? Request.Form[qqFileNameParameter].ToString()
                : string.Empty;

            StoredFileModel attachment = new StoredFileModel();
            if (httpPostedFile != null && httpPostedFile.Length > 0)
            {
                attachment = StoredFileService.CreateTempStoredFile();
                attachment.Name = System.IO.Path.GetFileName(httpPostedFile.FileName);
                attachment.MimeType = httpPostedFile.ContentType;
                attachment.Size = httpPostedFile.Length;
                attachment = StoredFileService.SaveStoredFile(attachment);
                FileInfo fileInFo = new FileInfo(httpPostedFile.FileName);
                string ext = fileInFo.Extension;
                string fileName = string.Format("{0}{1}", StoredFileService.GetDirectoryPath(attachment.Id), ext);
                await AzureStorageService.UploadAttachmentToStorage(httpPostedFile.OpenReadStream(), fileName);
            }


            if (attachment.Id == 0)
                return Json(new { success = false, message = "Wrong file format" });

            return Json(new
            {
                success = true,
                pictureId = attachment.Id,
                imageUrl = string.Format("{0}{1}","/StoredFile/DownloadAttachment/",attachment.Id)
            });
        }

        [HttpGet("DownloadAttachment")]
        public async Task<ActionResult> DownloadAttachment(int id, int attachmentId)
        {
            //AttachmentListModel attachmentViewModel = new AttachmentListModel();
            //attachmentViewModel = AttachmentEntityService.GetById(id, attachmentId);
            //FileInfo fileInFo = new FileInfo(attachmentViewModel.Name);
            //string ext = fileInFo.Extension;
            //string fileName = string.Format("{0}{1}", StoredFileService.GetDirectoryPath(attachmentViewModel.AttachmentId), ext);
            //if (AzureStorageService.HasAttachment(fileName).Result)
            //{
            //    Stream blobStream = await AzureStorageService.DownloadAttachmentFromStorage(fileName);
            //    return File(blobStream, attachmentViewModel.MimeType, attachmentViewModel.Name);
            //}
            //else
            //{
            //    return Content("Attachment does not exist");
            //}

            return RedirectToAction("Index");

        }

    }
}
