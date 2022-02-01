using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.IO;
using System.Threading.Tasks;
using Aircon.Business.Helper;

namespace Aircon.FileUpload.Service
{
    public interface IAzureStorageService
    {
        Task<bool> UploadImageToStorage(Stream fileStream, string fileName);
        string GetImageFromStorage(string fileName);
        Task<bool> HasImage(string fileName);
        Task<Stream> DownloadImageFromStorage(string fileName);
        Task<bool> UploadVideoToStorage(Stream fileStream, string fileName);
        string GetVideoFromStorage(string fileName);
        Task<bool> HasVideo(string fileName);
        Task<Stream> DownloadVideoFromStorage(string fileName);
        Task<bool> UploadAttachmentToStorage(Stream fileStream, string fileName);
        string GetAttachmentFromStorage(string fileName);
        Task<bool> HasAttachment(string fileName);
        Task<Stream> DownloadAttachmentFromStorage(string fileName);
        Task<bool> UploadLocationSurveyToStorage(Stream fileStream, string fileName);
        string GetLocationSurveyFromStorage(string fileName);
        Task<bool> HasLocationSurvey(string fileName);
        Task<Stream> DownloadLocationSurveyFromStorage(string fileName);
        Task<bool> HasTemplate(string fileName);
        Task<Stream> DownloadTemplates(string fileName);
    }

    public class AzureStorageService : IAzureStorageService
    {
        private static IConfigValueProvider configSectionProvider;
        private const string ImageMasterContainer = "imgmaster";
        private const string VideoMasterContainer = "videomaster";
        private const string AttachmentContainer = "attachment";
        private const string LocationSurveyContainer = "locationsurvey";
        private const string TemplateContainer = "templates";
        private const string StorageAccountKey = nameof(StorageAccountKey);
        private const string StorageAccountName = nameof(StorageAccountName);
        private const string mediaPathFormat = "{0}/{1}/{2}/{3}";
        public AzureStorageService(IConfigValueProvider _configSectionProvider)
        {
            configSectionProvider = _configSectionProvider;
        }

        private CloudBlockBlob GetCloudBlockBlob(string containerName, string fileName)
        {
            StorageCredentials storageCredentials = new StorageCredentials(configSectionProvider.GetValue(StorageAccountName), configSectionProvider.GetValue(StorageAccountKey));
            CloudStorageAccount storageAccount = new CloudStorageAccount(storageCredentials, true);
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference(containerName);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            return blockBlob;
        }

        public async Task<bool> UploadImageToStorage(Stream fileStream, string fileName)
        {
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(ImageMasterContainer, fileName);
            using (var s = fileStream)
            {
                await cloudBlockBlob.UploadFromStreamAsync(s);
            }
            return await Task.FromResult(true);
        }

        public string GetImageFromStorage(string fileName)
        {
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(ImageMasterContainer, fileName);
            var readPolicy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow + TimeSpan.FromMinutes(5)
            };
            var newUri = new Uri(cloudBlockBlob.Uri.AbsoluteUri + cloudBlockBlob.GetSharedAccessSignature(readPolicy));
            return newUri.ToString();
        }
        public async Task<bool> HasImage(string fileName)
        {
            var result = GetCloudBlockBlob(ImageMasterContainer, fileName).ExistsAsync();
            return await Task.FromResult(result.Result);
        }
        public async Task<Stream> DownloadImageFromStorage(string fileName)
        {
            MemoryStream ms = new MemoryStream();
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(ImageMasterContainer, fileName);
            if (await cloudBlockBlob.ExistsAsync())
            {
                await cloudBlockBlob.DownloadToStreamAsync(ms);
                Stream blobStream = cloudBlockBlob.OpenReadAsync().Result;
                return blobStream;
            }
            return ms;
        }       

        public async Task<bool> UploadVideoToStorage(Stream fileStream, string fileName)
        {
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(VideoMasterContainer, fileName);
            using (var s = fileStream)
            {
                await cloudBlockBlob.UploadFromStreamAsync(s);
            }
            return await Task.FromResult(true);
        }

        public string GetVideoFromStorage(string fileName)
        {
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(VideoMasterContainer, fileName);
            var readPolicy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow + TimeSpan.FromMinutes(5)
            };
            var newUri = new Uri(cloudBlockBlob.Uri.AbsoluteUri + cloudBlockBlob.GetSharedAccessSignature(readPolicy));
            return newUri.ToString();
        }
        public async Task<bool> HasVideo(string fileName)
        {
            var result = GetCloudBlockBlob(VideoMasterContainer, fileName).ExistsAsync();
            return await Task.FromResult(result.Result);
        }
        public async Task<Stream> DownloadVideoFromStorage(string fileName)
        {
            MemoryStream ms = new MemoryStream();
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(VideoMasterContainer, fileName);
            if (await cloudBlockBlob.ExistsAsync())
            {
                await cloudBlockBlob.DownloadToStreamAsync(ms);
                Stream blobStream = cloudBlockBlob.OpenReadAsync().Result;
                return blobStream;
            }
            return ms;
        }

        public async Task<bool> UploadAttachmentToStorage(Stream fileStream, string fileName)
        {
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(AttachmentContainer, fileName);
            using (var s = fileStream)
            {
                await cloudBlockBlob.UploadFromStreamAsync(s);
            }
            return await Task.FromResult(true);
        }

        public string GetAttachmentFromStorage(string fileName)
        {
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(AttachmentContainer, fileName);
            var readPolicy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow + TimeSpan.FromMinutes(5)
            };
            var newUri = new Uri(cloudBlockBlob.Uri.AbsoluteUri + cloudBlockBlob.GetSharedAccessSignature(readPolicy));
            return newUri.ToString();
        }
        public async Task<bool> HasAttachment(string fileName)
        {
            var result = GetCloudBlockBlob(AttachmentContainer, fileName).ExistsAsync();
            return await Task.FromResult(result.Result);
        }
        public async Task<Stream> DownloadAttachmentFromStorage(string fileName)
        {
            MemoryStream ms = new MemoryStream();
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(AttachmentContainer, fileName);
            if (await cloudBlockBlob.ExistsAsync())
            {
                await cloudBlockBlob.DownloadToStreamAsync(ms);
                Stream blobStream = cloudBlockBlob.OpenReadAsync().Result;
                return blobStream;
            }
            return ms;
        }
        public async Task<bool> UploadLocationSurveyToStorage(Stream fileStream, string fileName)
        {
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(LocationSurveyContainer, fileName);
            using (var s = fileStream)
            {
                await cloudBlockBlob.UploadFromStreamAsync(s);
            }
            return await Task.FromResult(true);
        }
        public string GetLocationSurveyFromStorage(string fileName)
        {
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(LocationSurveyContainer, fileName);
            var readPolicy = new SharedAccessBlobPolicy()
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow + TimeSpan.FromMinutes(5)
            };
            var newUri = new Uri(cloudBlockBlob.Uri.AbsoluteUri + cloudBlockBlob.GetSharedAccessSignature(readPolicy));
            return newUri.ToString();
        }
        public async Task<bool> HasLocationSurvey(string fileName)
        {
            var result = GetCloudBlockBlob(LocationSurveyContainer, fileName).ExistsAsync();
            return await Task.FromResult(result.Result);
        }
        public async Task<Stream> DownloadLocationSurveyFromStorage(string fileName)
        {
            MemoryStream ms = new MemoryStream();
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(LocationSurveyContainer, fileName);
            if (await cloudBlockBlob.ExistsAsync())
            {
                await cloudBlockBlob.DownloadToStreamAsync(ms);
                Stream blobStream = cloudBlockBlob.OpenReadAsync().Result;
                return blobStream;
            }
            return ms;
        }
        public async Task<bool> HasTemplate(string fileName)
        {
            var result = GetCloudBlockBlob(TemplateContainer, fileName).ExistsAsync();
            return await Task.FromResult(result.Result);
        }
        public async Task<Stream> DownloadTemplates(string fileName)
        {
            MemoryStream ms = new MemoryStream();
            CloudBlockBlob cloudBlockBlob = GetCloudBlockBlob(TemplateContainer, fileName);
            if (await cloudBlockBlob.ExistsAsync())
            {
                await cloudBlockBlob.DownloadToStreamAsync(ms);
                Stream blobStream = cloudBlockBlob.OpenReadAsync().Result;
                return blobStream;
            }
            return ms;
        }

    }

}
