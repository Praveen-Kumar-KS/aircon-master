using Aircon.Business.Extensions;
using Aircon.Business.Models.Shared;
using Aircon.Business.Services;
using Aircon.Data;
using Aircon.Data.Helper;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Media
{
    public interface IAttachmentService
    {
        Task<StoredFileModel> GetStoredFileByIdAsync(int id);
        Task<string> GetPictureUrlAsync(int id, int targetSize = 0);
        Task<string> GetPictureUrlAsync(PictureModel picture,
                    int targetSize = 0,
                    bool showDefaultPicture = true,
                    string defaultPictureType = PictureType.Entity);
        Task<string> GetDefaultPictureUrlAsync(PictureModel picture, int targetSize = 0);
        Task<StoredFileModel> InsertAttachementAsync(IFormFile formFile, string defaultFileName = "", string virtualPath = "");
    }


    public class AttachmentService : IAttachmentService
    {

        private readonly AirconDbContext _airconDbContext;
        private readonly IStoredFileService _storedFileService;
        private readonly IAirFileProvider _airFileProvider;

        public AttachmentService(AirconDbContext airconDbContext, IStoredFileService storedFileService, IAirFileProvider airFileProvider)
        {
            _airconDbContext = airconDbContext;
            _storedFileService = storedFileService;
            _airFileProvider = airFileProvider;
        }
        
        public async Task<StoredFileModel> GetStoredFileByIdAsync(int id)
        {
            return await _airconDbContext.Attachments.Where(x => x.Id == id).Select(x=> new StoredFileModel { 
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                Location = x.Location,
                MimeType = x.MimeType,
                Size = x.Size
            }).SingleOrDefaultAsync();
        }

        public async Task<string> GetPictureUrlAsync(PictureModel picture,
            int targetSize = 0,
            bool showDefaultPicture = true,
            string defaultPictureType = PictureType.Entity)
        {
            if (!picture.Id.HasValue || picture.Id.Value == 0)
                return await GetDefaultPictureUrlAsync(picture,targetSize);
            return await GetPictureUrlAsync(picture.Id.Value,targetSize);
        }

        public async Task<string> GetPictureUrlAsync(int id,int targetSize = 0)
        {
            var storedFile = _storedFileService.GetStoredFileById(id);
            var imagePath = await GetImagesPathUrlAsync();
            var url = string.Format("{0}{1}/{2}", imagePath, storedFile.Id, storedFile.Name);
            return await Task.FromResult(url);
        }

        public virtual async Task<string> GetDefaultPictureUrlAsync(PictureModel picture, int targetSize = 512)
        {
            if (picture.EntityId.HasValue)
            {
                string url = string.Format("/avatars/{0}/{1}/{2}", picture.PictureType,picture.EntityId,targetSize );
                return await Task.FromResult(url);
            }
            return await Task.FromResult("/images/default.svg");
        }

        public async Task<StoredFileModel> InsertAttachementAsync(IFormFile imageFile, string defaultFileName = "", string virtualPath = "")
        {

            StoredFileModel attachment = new StoredFileModel();
            if (imageFile != null && imageFile.Length > 0)
            {
                attachment = _storedFileService.CreateTempStoredFile();
                attachment.Name = System.IO.Path.GetFileName(imageFile.FileName);
                attachment.MimeType = imageFile.ContentType;
                attachment.Size = imageFile.Length;
                attachment = _storedFileService.SaveStoredFile(attachment);
                FileInfo fileInFo = new FileInfo(imageFile.FileName);
                string ext = fileInFo.Extension;
                string fileName = string.Format("{0}{1}", _storedFileService.GetDirectoryPath(attachment.Id), ext);
                await UploadAttachmentToStorage(imageFile.OpenReadStream(), fileName);
            }

            return await Task.FromResult(attachment);
        }

        protected async Task UploadAttachmentToStorage(Stream fileStream, string fileName)
        {
            await _airFileProvider.WriteAllBytesAsync(await GetPictureLocalPathAsync(fileName), fileStream.ReadAllBytes());
        }

        protected virtual Task<string> GetPictureLocalPathAsync(string fileName)
        {
            var localFileName = _airFileProvider.GetAbsolutePath("images", fileName);
            _airFileProvider.CreateDirectoryIfNotExists(localFileName);
            return Task.FromResult(localFileName);
        }


        protected virtual Task<string> GetImagesPathUrlAsync()
        {
            var pathBase = HttpContextHelper.Current.Request.PathBase.Value ?? string.Empty;
            var imagesPathUrl =  $"{pathBase}/";
            imagesPathUrl += "i/";
            return Task.FromResult(imagesPathUrl);
        }

    }
}