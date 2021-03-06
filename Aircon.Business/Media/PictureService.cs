//using Aircon.Business.Helper;
//using Aircon.Core;
//using Aircon.Data.Entities;
//using Microsoft.AspNetCore.Http;
//using SkiaSharp;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Aircon.Business.Media
//{
//    public partial class PictureService : IPictureService
//    {
//        #region Fields

//        private readonly INopDataProvider _dataProvider;
//        private readonly IDownloadService _downloadService;
//        private readonly IHttpContextAccessor _httpContextAccessor;
//        private readonly INopFileProvider _fileProvider;
//        private readonly IRepository<Picture> _pictureRepository;
//        private readonly IRepository<PictureBinary> _pictureBinaryRepository;
//        private readonly IRepository<ProductPicture> _productPictureRepository;
//        private readonly ISettingService _settingService;
//        private readonly IUrlRecordService _urlRecordService;
//        private readonly IWebHelper _webHelper;
//        private readonly MediaSettings _mediaSettings;

//        #endregion

//        #region Ctor

//        public PictureService(INopDataProvider dataProvider,
//            IDownloadService downloadService,
//            IHttpContextAccessor httpContextAccessor,
//            INopFileProvider fileProvider,
//            IRepository<Picture> pictureRepository,
//            IRepository<PictureBinary> pictureBinaryRepository,
//            IRepository<ProductPicture> productPictureRepository,
//            ISettingService settingService,
//            IUrlRecordService urlRecordService,
//            IWebHelper webHelper,
//            MediaSettings mediaSettings)
//        {
//            _dataProvider = dataProvider;
//            _downloadService = downloadService;
//            _httpContextAccessor = httpContextAccessor;
//            _fileProvider = fileProvider;
//            _pictureRepository = pictureRepository;
//            _pictureBinaryRepository = pictureBinaryRepository;
//            _productPictureRepository = productPictureRepository;
//            _settingService = settingService;
//            _urlRecordService = urlRecordService;
//            _webHelper = webHelper;
//            _mediaSettings = mediaSettings;
//        }

//        #endregion

//        #region Utilities

//        /// <summary>
//        /// Loads a picture from file
//        /// </summary>
//        /// <param name="pictureId">Picture identifier</param>
//        /// <param name="mimeType">MIME type</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture binary
//        /// </returns>
//        protected virtual async Task<byte[]> LoadPictureFromFileAsync(int pictureId, string mimeType)
//        {
//            var lastPart = await GetFileExtensionFromMimeTypeAsync(mimeType);
//            var fileName = $"{pictureId:0000000}_0.{lastPart}";
//            var filePath = await GetPictureLocalPathAsync(fileName);

//            return await _fileProvider.ReadAllBytesAsync(filePath);
//        }

//        /// <summary>
//        /// Save picture on file system
//        /// </summary>
//        /// <param name="pictureId">Picture identifier</param>
//        /// <param name="pictureBinary">Picture binary</param>
//        /// <param name="mimeType">MIME type</param>
//        /// <returns>A task that represents the asynchronous operation</returns>
//        protected virtual async Task SavePictureInFileAsync(int pictureId, byte[] pictureBinary, string mimeType)
//        {
//            var lastPart = await GetFileExtensionFromMimeTypeAsync(mimeType);
//            var fileName = $"{pictureId:0000000}_0.{lastPart}";
//            await _fileProvider.WriteAllBytesAsync(await GetPictureLocalPathAsync(fileName), pictureBinary);
//        }

//        /// <summary>
//        /// Delete a picture on file system
//        /// </summary>
//        /// <param name="picture">Picture</param>
//        /// <returns>A task that represents the asynchronous operation</returns>
//        protected virtual async Task DeletePictureOnFileSystemAsync(Attachment picture)
//        {
//            if (picture == null)
//                throw new ArgumentNullException(nameof(picture));

//            var lastPart = await GetFileExtensionFromMimeTypeAsync(picture.MimeType);
//            var fileName = $"{picture.Id:0000000}_0.{lastPart}";
//            var filePath = await GetPictureLocalPathAsync(fileName);
//            _fileProvider.DeleteFile(filePath);
//        }

//        /// <summary>
//        /// Delete picture thumbs
//        /// </summary>
//        /// <param name="picture">Picture</param>
//        /// <returns>A task that represents the asynchronous operation</returns>
//        protected virtual async Task DeletePictureThumbsAsync(Attachment picture)
//        {
//            var filter = $"{picture.Id:0000000}*.*";
//            var currentFiles = _fileProvider.GetFiles(_fileProvider.GetAbsolutePath(AirMediaDefaults.ImageThumbsPath), filter, false);
//            foreach (var currentFileName in currentFiles)
//            {
//                var thumbFilePath = await GetThumbLocalPathAsync(currentFileName);
//                _fileProvider.DeleteFile(thumbFilePath);
//            }
//        }

//        /// <summary>
//        /// Get picture (thumb) local path
//        /// </summary>
//        /// <param name="thumbFileName">Filename</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the local picture thumb path
//        /// </returns>
//        protected virtual Task<string> GetThumbLocalPathAsync(string thumbFileName)
//        {
//            var thumbsDirectoryPath = _fileProvider.GetAbsolutePath(AirMediaDefaults.ImageThumbsPath);

//            if (_mediaSettings.MultipleThumbDirectories)
//            {
//                //get the first two letters of the file name
//                var fileNameWithoutExtension = _fileProvider.GetFileNameWithoutExtension(thumbFileName);
//                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > AirMediaDefaults.MultipleThumbDirectoriesLength)
//                {
//                    var subDirectoryName = fileNameWithoutExtension[0..AirMediaDefaults.MultipleThumbDirectoriesLength];
//                    thumbsDirectoryPath = _fileProvider.GetAbsolutePath(AirMediaDefaults.ImageThumbsPath, subDirectoryName);
//                    _fileProvider.CreateDirectory(thumbsDirectoryPath);
//                }
//            }

//            var thumbFilePath = _fileProvider.Combine(thumbsDirectoryPath, thumbFileName);
//            return Task.FromResult(thumbFilePath);
//        }

//        /// <summary>
//        /// Get images path URL 
//        /// </summary>
//        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the 
//        /// </returns>
//        protected virtual Task<string> GetImagesPathUrlAsync(string storeLocation = null)
//        {
//            var pathBase = _httpContextAccessor.HttpContext.Request.PathBase.Value ?? string.Empty;
//            var imagesPathUrl = $"{pathBase}/";
//            imagesPathUrl += "images/";
//            return Task.FromResult(imagesPathUrl);
//        }

//        /// <summary>
//        /// Get picture (thumb) URL 
//        /// </summary>
//        /// <param name="thumbFileName">Filename</param>
//        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the local picture thumb path
//        /// </returns>
//        protected virtual async Task<string> GetThumbUrlAsync(string thumbFileName, string storeLocation = null)
//        {
//            var url = await GetImagesPathUrlAsync(storeLocation) + "thumbs/";

//            if (_mediaSettings.MultipleThumbDirectories)
//            {
//                //get the first two letters of the file name
//                var fileNameWithoutExtension = _fileProvider.GetFileNameWithoutExtension(thumbFileName);
//                if (fileNameWithoutExtension != null && fileNameWithoutExtension.Length > AirMediaDefaults.MultipleThumbDirectoriesLength)
//                {
//                    var subDirectoryName = fileNameWithoutExtension[0..AirMediaDefaults.MultipleThumbDirectoriesLength];
//                    url = url + subDirectoryName + "/";
//                }
//            }

//            url += thumbFileName;
//            return url;
//        }

//        /// <summary>
//        /// Get picture local path. Used when images stored on file system (not in the database)
//        /// </summary>
//        /// <param name="fileName">Filename</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the local picture path
//        /// </returns>
//        protected virtual Task<string> GetPictureLocalPathAsync(string fileName)
//        {
//            return Task.FromResult(_fileProvider.GetAbsolutePath("images", fileName));
//        }

//        /// <summary>
//        /// Gets the loaded picture binary depending on picture storage settings
//        /// </summary>
//        /// <param name="picture">Picture</param>
//        /// <param name="fromDb">Load from database; otherwise, from file system</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture binary
//        /// </returns>
//        protected virtual async Task<byte[]> LoadPictureBinaryAsync(Attachment picture)
//        {
//            if (picture == null)
//                throw new ArgumentNullException(nameof(picture));
//            var result =  await LoadPictureFromFileAsync(picture.Id, picture.MimeType);
//            return result;
//        }

//        /// <summary>
//        /// Get a value indicating whether some file (thumb) already exists
//        /// </summary>
//        /// <param name="thumbFilePath">Thumb file path</param>
//        /// <param name="thumbFileName">Thumb file name</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the result
//        /// </returns>
//        protected virtual Task<bool> GeneratedThumbExistsAsync(string thumbFilePath, string thumbFileName)
//        {
//            return Task.FromResult(_fileProvider.FileExists(thumbFilePath));
//        }

//        /// <summary>
//        /// Save a value indicating whether some file (thumb) already exists
//        /// </summary>
//        /// <param name="thumbFilePath">Thumb file path</param>
//        /// <param name="thumbFileName">Thumb file name</param>
//        /// <param name="mimeType">MIME type</param>
//        /// <param name="binary">Picture binary</param>
//        /// <returns>A task that represents the asynchronous operation</returns>
//        protected virtual async Task SaveThumbAsync(string thumbFilePath, string thumbFileName, string mimeType, byte[] binary)
//        {
//            //ensure \thumb directory exists
//            var thumbsDirectoryPath = _fileProvider.GetAbsolutePath(AirMediaDefaults.ImageThumbsPath);
//            _fileProvider.CreateDirectory(thumbsDirectoryPath);

//            //save
//            await _fileProvider.WriteAllBytesAsync(thumbFilePath, binary);
//        }


//        /// <summary>
//        /// Get image format by mime type
//        /// </summary>
//        /// <param name="mimetype">Mime type</param>
//        /// <returns>SKEncodedImageFormat</returns>
//        protected virtual SKEncodedImageFormat GetImageFormatByMimeType(string mimeType)
//        {
//            var format = SKEncodedImageFormat.Jpeg;
//            if (string.IsNullOrEmpty(mimeType))
//                return format;

//            var parts = mimeType.ToLower().Split('/');
//            var lastPart = parts[^1];

//            switch (lastPart)
//            {
//                case "webp":
//                    format = SKEncodedImageFormat.Webp;
//                    break;
//                case "png":
//                case "gif":
//                case "bmp":
//                case "x-icon":
//                    format = SKEncodedImageFormat.Png;
//                    break;
//                default:
//                    break;
//            }

//            return format;
//        }

//        /// <summary>
//        /// Resize image by targetSize
//        /// </summary>
//        /// <param name="image">Source image</param>
//        /// <param name="format">Destination format</param>
//        /// <param name="targetSize">Target size</param>
//        /// <returns>Image as array of byte[]</returns>
//        protected virtual byte[] ImageResize(SKBitmap image, SKEncodedImageFormat format, int targetSize)
//        {
//            if (image == null)
//                throw new ArgumentNullException("Image is null");

//            float width, height;
//            if (image.Height > image.Width)
//            {
//                // portrait
//                width = image.Width * (targetSize / (float)image.Height);
//                height = targetSize;
//            }
//            else
//            {
//                // landscape or square
//                width = targetSize;
//                height = image.Height * (targetSize / (float)image.Width);
//            }

//            if ((int)width == 0 || (int)height == 0)
//            {
//                width = image.Width;
//                height = image.Height;
//            }
//            try
//            {
//                using var resizedBitmap = image.Resize(new SKImageInfo((int)width, (int)height), SKFilterQuality.Medium);
//                using var cropImage = SKImage.FromBitmap(resizedBitmap);

//                //In order to exclude saving pictures in low quality at the time of installation, we will set the value of this parameter to 80 (as by default)
//                return cropImage.Encode(format, _mediaSettings.DefaultImageQuality > 0 ? _mediaSettings.DefaultImageQuality : 80).ToArray();
//            }
//            catch
//            {
//                return image.Bytes;
//            }

//        }

//        #endregion

//        #region Getting picture local path/URL methods

//        /// <summary>
//        /// Returns the file extension from mime type.
//        /// </summary>
//        /// <param name="mimeType">Mime type</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the file extension
//        /// </returns>
//        public virtual Task<string> GetFileExtensionFromMimeTypeAsync(string mimeType)
//        {
//            if (mimeType == null)
//                return Task.FromResult<string>(null);

//            var parts = mimeType.Split('/');
//            var lastPart = parts[^1];
//            switch (lastPart)
//            {
//                case "pjpeg":
//                    lastPart = "jpg";
//                    break;
//                case "x-png":
//                    lastPart = "png";
//                    break;
//                case "x-icon":
//                    lastPart = "ico";
//                    break;
//                default:
//                    break;
//            }

//            return Task.FromResult(lastPart);
//        }

//        public virtual async Task<string> GetPictureSeNameAsync(string name)
//        {
//            return await _urlRecordService.GetSeNameAsync(name, true, false);
//        }

//        /// <summary>
//        /// Gets the default picture URL
//        /// </summary>
//        /// <param name="targetSize">The target picture size (longest side)</param>
//        /// <param name="defaultPictureType">Default picture type</param>
//        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture URL
//        /// </returns>
//        public virtual async Task<string> GetDefaultPictureUrlAsync(int targetSize = 0,
//            PictureType defaultPictureType = PictureType.Entity,
//            string storeLocation = null)
//        {
//            var defaultImageFileName = defaultPictureType switch
//            {
//                PictureType.Avatar => await _settingService.GetSettingByKeyAsync("Media.Customer.DefaultAvatarImageName", AirMediaDefaults.DefaultAvatarFileName),
//                _ => await _settingService.GetSettingByKeyAsync("Media.DefaultImageName", AirMediaDefaults.DefaultImageFileName),
//            };
//            var filePath = await GetPictureLocalPathAsync(defaultImageFileName);
//            if (!_fileProvider.FileExists(filePath))
//            {
//                return string.Empty;
//            }

//            if (targetSize == 0)
//                return await GetImagesPathUrlAsync(storeLocation) + defaultImageFileName;

//            var fileExtension = _fileProvider.GetFileExtension(filePath);
//            var thumbFileName = $"{_fileProvider.GetFileNameWithoutExtension(filePath)}_{targetSize}{fileExtension}";
//            var thumbFilePath = await GetThumbLocalPathAsync(thumbFileName);
//            if (!await GeneratedThumbExistsAsync(thumbFilePath, thumbFileName))
//            {
//                //the named mutex helps to avoid creating the same files in different threads,
//                //and does not decrease performance significantly, because the code is blocked only for the specific file.
//                //you should be very careful, mutexes cannot be used in with the await operation
//                //we can't use semaphore here, because it produces PlatformNotSupportedException exception on UNIX based systems
//                using var mutex = new Mutex(false, thumbFileName);
//                mutex.WaitOne();
//                try
//                {
//                    using var image = SKBitmap.Decode(filePath);
//                    var codec = SKCodec.Create(filePath);
//                    var format = codec.EncodedFormat;
//                    var pictureBinary = ImageResize(image, format, targetSize);
//                    SaveThumbAsync(thumbFilePath, thumbFileName, string.Empty, pictureBinary).Wait();
//                }
//                finally
//                {
//                    mutex.ReleaseMutex();
//                }
//            }

//            return await GetThumbUrlAsync(thumbFileName, storeLocation);
//        }

//        /// <summary>
//        /// Get a picture URL
//        /// </summary>
//        /// <param name="pictureId">Picture identifier</param>
//        /// <param name="targetSize">The target picture size (longest side)</param>
//        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
//        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
//        /// <param name="defaultPictureType">Default picture type</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture URL
//        /// </returns>
//        public virtual async Task<string> GetPictureUrlAsync(int pictureId,
//            int targetSize = 0,
//            bool showDefaultPicture = true,
//            string storeLocation = null,
//            PictureType defaultPictureType = PictureType.Entity)
//        {
//            var picture = await GetPictureByIdAsync(pictureId);
//            return (await GetPictureUrlAsync(picture, targetSize, showDefaultPicture, storeLocation, defaultPictureType)).Url;
//        }

//        /// <summary>
//        /// Get a picture URL
//        /// </summary>
//        /// <param name="picture">Reference instance of Picture</param>
//        /// <param name="targetSize">The target picture size (longest side)</param>
//        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
//        /// <param name="storeLocation">Store location URL; null to use determine the current store location automatically</param>
//        /// <param name="defaultPictureType">Default picture type</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture URL
//        /// </returns>
//        public virtual async Task<(string Url, Attachment Picture)> GetPictureUrlAsync(Attachment picture,
//            int targetSize = 0,
//            bool showDefaultPicture = true,
//            string storeLocation = null,
//            PictureType defaultPictureType = PictureType.Entity)
//        {
//            if (picture == null)
//                return showDefaultPicture ? (await GetDefaultPictureUrlAsync(targetSize, defaultPictureType, storeLocation), null) : (string.Empty, (Picture)null);

//            byte[] pictureBinary = null;
//            if (picture.Active)
//            {
//                await DeletePictureThumbsAsync(picture);
//                pictureBinary = await LoadPictureBinaryAsync(picture);

//                if ((pictureBinary?.Length ?? 0) == 0)
//                    return showDefaultPicture ? (await GetDefaultPictureUrlAsync(targetSize, defaultPictureType, storeLocation), picture) : (string.Empty, picture);

//                //we do not validate picture binary here to ensure that no exception ("Parameter is not valid") will be thrown
//                picture = await UpdatePictureAsync(picture.Id,
//                    pictureBinary,
//                    picture.MimeType,
//                    picture.SeoFilename,
//                    picture.AltAttribute,
//                    picture.TitleAttribute,
//                    false,
//                    false);
//            }

//            var seoFileName = picture.SeoFilename; // = GetPictureSeName(picture.SeoFilename); //just for sure

//            var lastPart = await GetFileExtensionFromMimeTypeAsync(picture.MimeType);
//            string thumbFileName;
//            if (targetSize == 0)
//            {
//                thumbFileName = !string.IsNullOrEmpty(seoFileName)
//                    ? $"{picture.Id:0000000}_{seoFileName}.{lastPart}"
//                    : $"{picture.Id:0000000}.{lastPart}";

//                var thumbFilePath = await GetThumbLocalPathAsync(thumbFileName);
//                if (await GeneratedThumbExistsAsync(thumbFilePath, thumbFileName))
//                    return (await GetThumbUrlAsync(thumbFileName, storeLocation), picture);

//                pictureBinary ??= await LoadPictureBinaryAsync(picture);

//                //the named mutex helps to avoid creating the same files in different threads,
//                //and does not decrease performance significantly, because the code is blocked only for the specific file.
//                //you should be very careful, mutexes cannot be used in with the await operation
//                //we can't use semaphore here, because it produces PlatformNotSupportedException exception on UNIX based systems
//                using var mutex = new Mutex(false, thumbFileName);
//                mutex.WaitOne();
//                try
//                {
//                    SaveThumbAsync(thumbFilePath, thumbFileName, string.Empty, pictureBinary).Wait();
//                }
//                finally
//                {
//                    mutex.ReleaseMutex();
//                }
//            }
//            else
//            {
//                thumbFileName = !string.IsNullOrEmpty(seoFileName)
//                    ? $"{picture.Id:0000000}_{seoFileName}_{targetSize}.{lastPart}"
//                    : $"{picture.Id:0000000}_{targetSize}.{lastPart}";

//                var thumbFilePath = await GetThumbLocalPathAsync(thumbFileName);
//                if (await GeneratedThumbExistsAsync(thumbFilePath, thumbFileName))
//                    return (await GetThumbUrlAsync(thumbFileName, storeLocation), picture);

//                pictureBinary ??= await LoadPictureBinaryAsync(picture);

//                //the named mutex helps to avoid creating the same files in different threads,
//                //and does not decrease performance significantly, because the code is blocked only for the specific file.
//                //you should be very careful, mutexes cannot be used in with the await operation
//                //we can't use semaphore here, because it produces PlatformNotSupportedException exception on UNIX based systems
//                using var mutex = new Mutex(false, thumbFileName);
//                mutex.WaitOne();
//                try
//                {
//                    if (pictureBinary != null)
//                    {
//                        try
//                        {
//                            using var image = SKBitmap.Decode(pictureBinary);
//                            var format = GetImageFormatByMimeType(picture.MimeType);
//                            pictureBinary = ImageResize(image, format, targetSize);
//                        }
//                        catch
//                        {
//                        }
//                    }

//                    SaveThumbAsync(thumbFilePath, thumbFileName, string.Empty, pictureBinary).Wait();
//                }
//                finally
//                {
//                    mutex.ReleaseMutex();
//                }
//            }

//            return (await GetThumbUrlAsync(thumbFileName, storeLocation), picture);
//        }

//        /// <summary>
//        /// Get a picture local path
//        /// </summary>
//        /// <param name="picture">Picture instance</param>
//        /// <param name="targetSize">The target picture size (longest side)</param>
//        /// <param name="showDefaultPicture">A value indicating whether the default picture is shown</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the 
//        /// </returns>
//        public virtual async Task<string> GetThumbLocalPathAsync(Attachment picture, int targetSize = 0, bool showDefaultPicture = true)
//        {
//            var (url, _) = await GetPictureUrlAsync(picture, targetSize, showDefaultPicture);
//            if (string.IsNullOrEmpty(url))
//                return string.Empty;

//            return await GetThumbLocalPathAsync(_fileProvider.GetFileName(url));
//        }

//        #endregion

//        #region CRUD methods

//        /// <summary>
//        /// Gets a picture
//        /// </summary>
//        /// <param name="pictureId">Picture identifier</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture
//        /// </returns>
//        public virtual async Task<Attachment> GetPictureByIdAsync(int pictureId)
//        {
//            return await _pictureRepository.GetByIdAsync(pictureId, cache => default);
//        }

//        /// <summary>
//        /// Deletes a picture
//        /// </summary>
//        /// <param name="picture">Picture</param>
//        /// <returns>A task that represents the asynchronous operation</returns>
//        public virtual async Task DeletePictureAsync(Attachment picture)
//        {
//            if (picture == null)
//                throw new ArgumentNullException(nameof(picture));

//            //delete thumbs
//            await DeletePictureThumbsAsync(picture);

//            await DeletePictureOnFileSystemAsync(picture);
//            //delete from database
//            await _pictureRepository.DeleteAsync(picture);
//        }

//        /// <summary>
//        /// Gets a collection of pictures
//        /// </summary>
//        /// <param name="virtualPath">Virtual path</param>
//        /// <param name="pageIndex">Current page</param>
//        /// <param name="pageSize">Items on each page</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the paged list of pictures
//        /// </returns>
//        //public virtual async Task<IPagedList<Picture>> GetPicturesAsync(string virtualPath = "", int pageIndex = 0, int pageSize = int.MaxValue)
//        //{
//        //    var query = _pictureRepository.Table;

//        //    if (!string.IsNullOrEmpty(virtualPath))
//        //        query = virtualPath.EndsWith('/') ? query.Where(p => p.VirtualPath.StartsWith(virtualPath) || p.VirtualPath == virtualPath.TrimEnd('/')) : query.Where(p => p.VirtualPath == virtualPath);

//        //    query = query.OrderByDescending(p => p.Id);

//        //    return await query.ToPagedListAsync(pageIndex, pageSize);
//        //}

//        /// <summary>
//        /// Gets pictures by product identifier
//        /// </summary>
//        /// <param name="productId">Product identifier</param>
//        /// <param name="recordsToReturn">Number of records to return. 0 if you want to get all items</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the pictures
//        /// </returns>
//        public virtual async Task<IList<Attachment>> GetPicturesByProductIdAsync(int productId, int recordsToReturn = 0)
//        {
//            if (productId == 0)
//                return new List<Attachment>();

//            var query = from p in _pictureRepository.Table
//                        join pp in _productPictureRepository.Table on p.Id equals pp.PictureId
//                        orderby pp.DisplayOrder, pp.Id
//                        where pp.ProductId == productId
//                        select p;

//            if (recordsToReturn > 0)
//                query = query.Take(recordsToReturn);

//            var pics = await query.ToListAsync();

//            return pics;
//        }

//        /// <summary>
//        /// Inserts a picture
//        /// </summary>
//        /// <param name="pictureBinary">The picture binary</param>
//        /// <param name="mimeType">The picture MIME type</param>
//        /// <param name="seoFilename">The SEO filename</param>
//        /// <param name="altAttribute">"alt" attribute for "img" HTML element</param>
//        /// <param name="titleAttribute">"title" attribute for "img" HTML element</param>
//        /// <param name="isNew">A value indicating whether the picture is new</param>
//        /// <param name="validateBinary">A value indicating whether to validated provided picture binary</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture
//        /// </returns>
//        public virtual async Task<Attachment> InsertPictureAsync(byte[] pictureBinary, string mimeType, string seoFilename,
//            string altAttribute = null, string titleAttribute = null,
//            bool isNew = true, bool validateBinary = true)
//        {
//            mimeType = CommonHelper.EnsureNotNull(mimeType);
//            mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

//            seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);

//            if (validateBinary)
//                pictureBinary = await ValidatePictureAsync(pictureBinary, mimeType);

//            var picture = new Attachment
//            {
//                MimeType = mimeType,
//                Name = seoFilename,
//                Description = titleAttribute,
//                Active = isNew
//            };
//            await _pictureRepository.InsertAsync(picture);
//            await SavePictureInFileAsync(picture.Id, pictureBinary, mimeType);
//            return picture;
//        }

//        /// <summary>
//        /// Inserts a picture
//        /// </summary>
//        /// <param name="formFile">Form file</param>
//        /// <param name="defaultFileName">File name which will be use if IFormFile.FileName not present</param>
//        /// <param name="virtualPath">Virtual path</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture
//        /// </returns>
//        public virtual async Task<Attachment> InsertPictureAsync(IFormFile formFile, string defaultFileName = "", string virtualPath = "")
//        {
//            var imgExt = new List<string>
//            {
//                ".bmp",
//                ".gif",
//                ".webp",
//                ".jpeg",
//                ".jpg",
//                ".jpe",
//                ".jfif",
//                ".pjpeg",
//                ".pjp",
//                ".png",
//                ".tiff",
//                ".tif"
//            } as IReadOnlyCollection<string>;

//            var fileName = formFile.FileName;
//            if (string.IsNullOrEmpty(fileName) && !string.IsNullOrEmpty(defaultFileName))
//                fileName = defaultFileName;

//            //remove path (passed in IE)
//            fileName = _fileProvider.GetFileName(fileName);

//            var contentType = formFile.ContentType;

//            var fileExtension = _fileProvider.GetFileExtension(fileName);
//            if (!string.IsNullOrEmpty(fileExtension))
//                fileExtension = fileExtension.ToLowerInvariant();

//            if (imgExt.All(ext => !ext.Equals(fileExtension, StringComparison.CurrentCultureIgnoreCase)))
//                return null;

//            //contentType is not always available 
//            //that's why we manually update it here
//            //http://www.sfsu.edu/training/mimetype.htm
//            if (string.IsNullOrEmpty(contentType))
//            {
//                switch (fileExtension)
//                {
//                    case ".bmp":
//                        contentType = MimeTypes.ImageBmp;
//                        break;
//                    case ".gif":
//                        contentType = MimeTypes.ImageGif;
//                        break;
//                    case ".jpeg":
//                    case ".jpg":
//                    case ".jpe":
//                    case ".jfif":
//                    case ".pjpeg":
//                    case ".pjp":
//                        contentType = MimeTypes.ImageJpeg;
//                        break;
//                    case ".webp":
//                        contentType = MimeTypes.ImageWebp;
//                        break;
//                    case ".png":
//                        contentType = MimeTypes.ImagePng;
//                        break;
//                    case ".tiff":
//                    case ".tif":
//                        contentType = MimeTypes.ImageTiff;
//                        break;
//                    default:
//                        break;
//                }
//            }

//            var picture = await InsertPictureAsync(await _downloadService.GetDownloadBitsAsync(formFile), contentType, _fileProvider.GetFileNameWithoutExtension(fileName));

//            if (string.IsNullOrEmpty(virtualPath))
//                return picture;

//            picture.VirtualPath = _fileProvider.GetVirtualPath(virtualPath);
//            await UpdatePictureAsync(picture);

//            return picture;
//        }

//        /// <summary>
//        /// Updates the picture
//        /// </summary>
//        /// <param name="pictureId">The picture identifier</param>
//        /// <param name="pictureBinary">The picture binary</param>
//        /// <param name="mimeType">The picture MIME type</param>
//        /// <param name="seoFilename">The SEO filename</param>
//        /// <param name="altAttribute">"alt" attribute for "img" HTML element</param>
//        /// <param name="titleAttribute">"title" attribute for "img" HTML element</param>
//        /// <param name="isNew">A value indicating whether the picture is new</param>
//        /// <param name="validateBinary">A value indicating whether to validated provided picture binary</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture
//        /// </returns>
//        public virtual async Task<Attachment> UpdatePictureAsync(int pictureId, byte[] pictureBinary, string mimeType,
//            string seoFilename, string altAttribute = null, string titleAttribute = null,
//            bool isNew = true, bool validateBinary = true)
//        {
//            mimeType = CommonHelper.EnsureNotNull(mimeType);
//            mimeType = CommonHelper.EnsureMaximumLength(mimeType, 20);

//            seoFilename = CommonHelper.EnsureMaximumLength(seoFilename, 100);

//            if (validateBinary)
//                pictureBinary = await ValidatePictureAsync(pictureBinary, mimeType);

//            var picture = await GetPictureByIdAsync(pictureId);
//            if (picture == null)
//                return null;

//            //delete old thumbs if a picture has been changed
//            if (seoFilename != picture.SeoFilename)
//                await DeletePictureThumbsAsync(picture);

//            picture.MimeType = mimeType;
//            picture.SeoFilename = seoFilename;
//            picture.AltAttribute = altAttribute;
//            picture.TitleAttribute = titleAttribute;
//            picture.IsNew = isNew;

//            await _pictureRepository.UpdateAsync(picture);
//            await SavePictureInFileAsync(picture.Id, pictureBinary, mimeType);
//            return picture;
//        }

//        /// <summary>
//        /// Updates the picture
//        /// </summary>
//        /// <param name="picture">The picture to update</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture
//        /// </returns>
//        public virtual async Task<Attachment> UpdatePictureAsync(Attachment picture)
//        {
//            if (picture == null)
//                return null;

//            var seoFilename = CommonHelper.EnsureMaximumLength(picture.SeoFilename, 100);

//            //delete old thumbs if a picture has been changed
//            if (seoFilename != picture.SeoFilename)
//                await DeletePictureThumbsAsync(picture);

//            picture.SeoFilename = seoFilename;

//            await _pictureRepository.UpdateAsync(picture);

//            await SavePictureInFileAsync(picture.Id, (await GetPictureBinaryByPictureIdAsync(picture.Id)).BinaryData, picture.MimeType);


//            return picture;
//        }

//        /// <summary>
//        /// Updates a SEO filename of a picture
//        /// </summary>
//        /// <param name="pictureId">The picture identifier</param>
//        /// <param name="seoFilename">The SEO filename</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture
//        /// </returns>
//        public virtual async Task<Attachment> SetSeoFilenameAsync(int pictureId, string seoFilename)
//        {
//            var picture = await GetPictureByIdAsync(pictureId);
//            if (picture == null)
//                throw new ArgumentException("No picture found with the specified id");

//            //update if it has been changed
//            if (seoFilename != picture.SeoFilename)
//            {
//                //update picture
//                picture = await UpdatePictureAsync(picture.Id,
//                    await LoadPictureBinaryAsync(picture),
//                    picture.MimeType,
//                    seoFilename,
//                    picture.AltAttribute,
//                    picture.TitleAttribute,
//                    true,
//                    false);
//            }

//            return picture;
//        }

//        /// <summary>
//        /// Validates input picture dimensions
//        /// </summary>
//        /// <param name="pictureBinary">Picture binary</param>
//        /// <param name="mimeType">MIME type</param>
//        /// <returns>
//        /// A task that represents the asynchronous operation
//        /// The task result contains the picture binary or throws an exception
//        /// </returns>
//        public virtual Task<byte[]> ValidatePictureAsync(byte[] pictureBinary, string mimeType)
//        {
//            try
//            {
//                using var image = SKBitmap.Decode(pictureBinary);

//                //resize the image in accordance with the maximum size
//                if (Math.Max(image.Height, image.Width) > _mediaSettings.MaximumImageSize)
//                {
//                    var format = GetImageFormatByMimeType(mimeType);
//                    pictureBinary = ImageResize(image, format, _mediaSettings.MaximumImageSize);
//                }
//                return Task.FromResult(pictureBinary);
//            }
//            catch
//            {
//                return Task.FromResult(pictureBinary);
//            }
//        }
//        #endregion
//    }
//}
