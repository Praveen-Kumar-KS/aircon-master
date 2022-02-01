//using Aircon.Data.Entities;
//using Microsoft.AspNetCore.Http;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Aircon.Business.Media
//{
//    public partial interface IPictureService
//    {
//        Task<string> GetFileExtensionFromMimeTypeAsync(string mimeType);

//        Task<byte[]> LoadPictureBinaryAsync(Attachment picture);

//        Task<string> GetPictureSeNameAsync(string name);
        
//        // picure.cshtml
//        Task<string> GetDefaultPictureUrlAsync(int targetSize = 0,
//            PictureType defaultPictureType = PictureType.Entity,
//            string storeLocation = null);
//        // picuture.cshtl
//        Task<string> GetPictureUrlAsync(int pictureId,
//            int targetSize = 0,
//            bool showDefaultPicture = true,
//            string storeLocation = null,
//            PictureType defaultPictureType = PictureType.Entity);
//        // picture.cshtml
//        Task<(string Url, Attachment Picture)> GetPictureUrlAsync(Attachment picture,
//            int targetSize = 0,
//            bool showDefaultPicture = true,
//            string storeLocation = null,
//            PictureType defaultPictureType = PictureType.Entity);

//        Task<string> GetThumbLocalPathAsync(Attachment picture, int targetSize = 0, bool showDefaultPicture = true);

//        // picture.cshtml
//        Task<Attachment> GetPictureByIdAsync(int pictureId);

//        Task DeletePictureAsync(Attachment picture);

//        //Task<IPagedList<Attachment>> GetPicturesAsync(string virtualPath = "", int pageIndex = 0, int pageSize = int.MaxValue);

//        Task<Attachment> InsertPictureAsync(byte[] pictureBinary, string mimeType, string seoFilename,
//            string altAttribute = null, string titleAttribute = null,
//            bool isNew = true, bool validateBinary = true);

//        Task<Attachment> InsertPictureAsync(IFormFile formFile, string defaultFileName = "", string virtualPath = "");

//        Task<Attachment> UpdatePictureAsync(int pictureId, byte[] pictureBinary, string mimeType,
//            string seoFilename, string altAttribute = null, string titleAttribute = null,
//            bool isNew = true, bool validateBinary = true);

//        Task<Attachment> UpdatePictureAsync(Attachment picture);

//        Task<Attachment> SetSeoFilenameAsync(int pictureId, string seoFilename);

//        Task<byte[]> ValidatePictureAsync(byte[] pictureBinary, string mimeType);


//    }

//}
