using Aircon.Business.Services;
using Aircon.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aircon.Business.Media
{
    public class ImageMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly PathString _endpoint;
        private readonly ILogger<ImageMiddleware> _log;

        //https://andrewlock.net/accessing-route-values-in-endpoint-middleware-in-aspnetcore-3/
        /// <summary>
        /// 
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="next"></param>
        /// <param name="avatarService"></param>
        /// <param name="log"></param>
        public ImageMiddleware(PathString endpoint, RequestDelegate next, ILogger<ImageMiddleware> log)
        {
            _endpoint = endpoint;
            _next = next;
            _log = log;
        }

        public async Task Invoke(HttpContext httpContext, IStoredFileService storedFileService, IAirFileProvider airFileProvider)
        {
            var request = httpContext.Request;
            if (!request.Path.StartsWithSegments(_endpoint, out var restOfPath))
            {
                await _next(httpContext);
                return;
            }
            var maybeValues = ParseUserIdSquareSize(httpContext);
            var (storedFileId, fileName) = maybeValues.Value;
            var formatExtension = airFileProvider.GetFileExtension(fileName);
            var filePath = string.Format("{0}{1}", storedFileService.GetDirectoryPath(storedFileId),formatExtension);
            var localFileName = airFileProvider.GetAbsolutePath("images", filePath);



            var buffer = await airFileProvider.ReadAllBytesAsync(localFileName);
            var response = httpContext.Response;
            response.Headers.Add("Content-Type", GetMimeType(formatExtension));
            response.Headers.Add("Cache-Control", "public,max-age=31536000");
            response.Headers.Add("Content-Length", buffer.Length.ToString(CultureInfo.InvariantCulture));
            await response.Body.WriteAsync(buffer, 0, buffer.Length);
        }

        private string GetMimeType(string formatExtension)
        {
            switch (formatExtension)
            {
                case ".png":
                    return "image/png";
                case ".webp":
                    return "image/webp";
                case ".svg":
                    return "image/svg+xml";
                default:
                    throw new InvalidOperationException("Invalid AvatarFormat specified.");
            }
        }

        private (int storedFileId, string fileName)? ParseUserIdSquareSize(HttpContext context)
        {

            var path = context.Request.Path;
            if (!path.HasValue) return null; // e.g. /random, /random/

            var segments = path.Value.Split('/');

            if (segments.Length < 3) return null; // e.g. /random/12, /random/blah, /random/123/12/tada
            System.Diagnostics.Debug.Assert(string.IsNullOrEmpty(segments[0])); // first segment is always empty
            if (!int.TryParse(segments[2], out var storedFileId)) return null; // e.g. /random/blah/123
            var fileName = segments[3].ToString();
            return (storedFileId, fileName);
        }
    }

}
