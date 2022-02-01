using Aircon.Business.Media;
using Aircon.Business.Services;
using Aircon.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Avatar
{
    public class AvatarMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly PathString _endpoint;
        private readonly IAvatarService _avatarService;
        private readonly ILogger<AvatarMiddleware> _log;
        //https://andrewlock.net/accessing-route-values-in-endpoint-middleware-in-aspnetcore-3/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="next"></param>
        /// <param name="avatarService"></param>
        /// <param name="log"></param>
        public AvatarMiddleware(PathString endpoint, RequestDelegate next, IAvatarService avatarService, ILogger<AvatarMiddleware> log)
        {
            _endpoint = endpoint;
            _next = next;
            _avatarService = avatarService;
            _log = log;
        }

        public async Task Invoke(HttpContext httpContext, AirconDbContext airconDbContext, IStoredFileService storedFileService, IAirFileProvider airFileProvider, IAvatarUserService avatarUserService)
        {
            var request = httpContext.Request;
            if (!request.Path.StartsWithSegments(_endpoint, out var restOfPath))
            {
                await _next(httpContext);
                return;
            }
            var maybeValues = ParseUserIdSquareSize(httpContext);

            var avatarName = string.Empty;
            byte[] buffer = Array.Empty<byte>();
            var formatExtension = "svg";
            var (avatarType, entityId, squareSize) = maybeValues.Value;
            if (maybeValues.HasValue)
            {
                if( PictureType.UserAvatar.Equals(avatarType))
                {
                    var user = await avatarUserService.GetUser(entityId);
                    if (user != null)
                    {
                        if (user.AvatarId.HasValue)
                        {
                            var storedFile = storedFileService.GetStoredFileById(user.AvatarId.Value);
                            var extension = airFileProvider.GetFileExtension(storedFile.Name);
                            var filePath = string.Format("{0}{1}", storedFileService.GetDirectoryPath(storedFile.Id), extension);
                            var localFileName = airFileProvider.GetAbsolutePath("images", filePath);
                            buffer = await airFileProvider.ReadAllBytesAsync(localFileName);
                            formatExtension = extension;
                        }
                        else
                        {
                            avatarName = string.Format("{0} {1}", user.FirstName, user.LastName);
                            buffer = await _avatarService.GenerateAvatar(avatarName, formatExtension, squareSize, httpContext.RequestAborted);
                        }

                    }
                }else if(PictureType.CompanyLogo.Equals(avatarType))
                {
                    var customer = airconDbContext.Customers.Find(entityId);

                    if (customer != null)
                    {
                        if (customer.LogoId.HasValue)
                        {
                            var storedFile = storedFileService.GetStoredFileById(customer.LogoId.Value);
                            var extension = airFileProvider.GetFileExtension(storedFile.Name);
                            var filePath = string.Format("{0}{1}", storedFileService.GetDirectoryPath(storedFile.Id), extension);
                            var localFileName = airFileProvider.GetAbsolutePath("images", filePath);
                            buffer = await airFileProvider.ReadAllBytesAsync(localFileName);
                            formatExtension = extension;
                        }
                        else
                        {
                            avatarName = customer.CompanyName;
                            buffer = await _avatarService.GenerateAvatar(avatarName, formatExtension, squareSize, httpContext.RequestAborted);
                        }
                    }
                }
            }

            if (string.IsNullOrEmpty(avatarName))
            {
                avatarName = "X X";
            }
            

            var response = httpContext.Response;
            response.Headers.Add("Content-Type", GetMimeType(formatExtension));
            response.Headers.Add("Cache-Control", "public,max-age=31536000");
            response.Headers.Add("Content-Length", buffer.Length.ToString(CultureInfo.InvariantCulture));
            await response.Body.WriteAsync(buffer, 0, buffer.Length);
        }

        private string GetAvatarFormat(string name)
        {
            var extension = Path.GetExtension(name).ToLowerInvariant(); ;

            switch (extension)
            {
                case ".svg":
                    return "svg";
                case ".webp":
                    return "webp";
                case ".png":
                    return "png";
            }

            // Browsers doesn't seem to advertise svg support in the Accept header (since all the major ones have
            // it) so we'll just fall back to it.

            return "svg";
        }

        private string GetMimeType(string formatExtension)
        {
            switch (formatExtension)
            {
                case "png":
                case ".png":
                    return "image/png";
                case "webp":
                case ".webp":
                    return "image/webp";
                case "svg":
                case ".svg":
                    return "image/svg+xml";
                default:
                    throw new InvalidOperationException("Invalid AvatarFormat specified.");
            }
        }

        private (string avatarType,int userId, int targetSize)? ParseUserIdSquareSize(HttpContext context)
        {

            var path = context.Request.Path;
            if (!path.HasValue) return null; // e.g. /random, /random/

            var segments = path.Value.Split('/');

            if (segments.Length < 3 ) return null; // e.g. /random/12, /random/blah, /random/123/12/tada
            System.Diagnostics.Debug.Assert(string.IsNullOrEmpty(segments[0])); // first segment is always empty
            var avatarType = segments[2].ToString();
            if (!int.TryParse(segments[3], out var userId)) return null; // e.g. /random/blah/123
            if (!int.TryParse(segments[4], out var targetSize)) return null; // e.g. /random/123/blah

            return (avatarType,userId, targetSize);
        }
    }

}
