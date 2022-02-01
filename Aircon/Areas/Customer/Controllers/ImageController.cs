using Aircon.Business.Media;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aircon.Areas.Customer.Controllers
{
    public class ImageController : BaseCustomerController
    {
        private readonly IAttachmentService _pictureService;

        public ImageController(IAttachmentService pictureService)
        {
            _pictureService = pictureService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
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

            var picture = await _pictureService.InsertAttachementAsync(httpPostedFile, qqFileName);

            //when returning JSON the mime-type must be set to text/plain
            //otherwise some browsers will pop-up a "Save As" dialog.

            if (picture == null)
                return Json(new { success = false, message = "Wrong file format" });

            return Json(new
            {
                success = true,
                pictureId = picture.Id,
                imageUrl = (await _pictureService.GetPictureUrlAsync(picture.Id, 100))
            });
        }


    }
}
