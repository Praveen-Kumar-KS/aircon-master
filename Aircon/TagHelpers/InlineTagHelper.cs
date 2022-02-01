using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Aircon.TagHelpers
{
    public abstract class InlineTagHelper : TagHelper
    {
        private const string CacheKeyPrefix = "InlineTagHelper-";

        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMemoryCache _cache;

        protected InlineTagHelper(IWebHostEnvironment hostingEnvironment, IMemoryCache cache)
        {
            _hostingEnvironment = hostingEnvironment;
            _cache = cache;
        }

        private async Task<T> GetContentAsync<T>(ICacheEntry entry, string path, Func<IFileInfo, Task<T>> getContent)
        {
            var fileProvider = _hostingEnvironment.WebRootFileProvider;
            var changeToken = fileProvider.Watch(path);

            entry.SetPriority(CacheItemPriority.NeverRemove);
            entry.AddExpirationToken(changeToken);

            var file = fileProvider.GetFileInfo(path);
            if (file == null || !file.Exists)
                return default(T);

            return await getContent(file);
        }

        protected Task<string> GetFileContentAsync(string path)
        {
            return _cache.GetOrCreateAsync(CacheKeyPrefix + path, entry =>
            {
                return GetContentAsync(entry, path, ReadFileContentAsStringAsync);
            });
        }

        protected Task<string> GetFileContentBase64Async(string path)
        {
            return _cache.GetOrCreateAsync(CacheKeyPrefix + path, entry =>
            {
                return GetContentAsync(entry, path, ReadFileContentAsBase64Async);
            });
        }

        private static async Task<string> ReadFileContentAsStringAsync(IFileInfo file)
        {
            using (var stream = file.CreateReadStream())
            using (var textReader = new StreamReader(stream))
            {
                return await textReader.ReadToEndAsync();
            }
        }

        private static async Task<string> ReadFileContentAsBase64Async(IFileInfo file)
        {
            using (var stream = file.CreateReadStream())
            using (var writer = new MemoryStream())
            {
                await stream.CopyToAsync(writer);
                writer.Seek(0, SeekOrigin.Begin);
                return Convert.ToBase64String(writer.ToArray());
            }
        }
    }

    public class InlineScriptTagHelper : InlineTagHelper
    {
        [HtmlAttributeName("src")]
        public string Src { get; set; }

        public InlineScriptTagHelper(IWebHostEnvironment hostingEnvironment, IMemoryCache cache)
            : base(hostingEnvironment, cache)
        {
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var fileContent = await GetFileContentAsync(Src);
            if (fileContent == null)
            {
                output.SuppressOutput();
                return;
            }

            output.TagName = "script";
            output.Attributes.RemoveAll("src");
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.AppendHtml(fileContent);
        }
    }

    public class InlineStyleTagHelper : InlineTagHelper
    {
        [HtmlAttributeName("href")]
        public string Href { get; set; }

        public InlineStyleTagHelper(IWebHostEnvironment hostingEnvironment, IMemoryCache cache)
            : base(hostingEnvironment, cache)
        {
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var fileContent = await GetFileContentAsync(Href);
            if (fileContent == null)
            {
                output.SuppressOutput();
                return;
            }

            output.TagName = "style";
            output.Attributes.RemoveAll("href");
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.AppendHtml(fileContent);
        }
    }

    public class InlineImgTagHelper : InlineTagHelper
    {
        private static readonly FileExtensionContentTypeProvider s_contentTypeProvider = new FileExtensionContentTypeProvider();

        [HtmlAttributeName("src")]
        public string Src { get; set; }

        public InlineImgTagHelper(IWebHostEnvironment hostingEnvironment, IMemoryCache cache)
            : base(hostingEnvironment, cache)
        {
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var fileContent = await GetFileContentBase64Async(Src);
            if (fileContent == null)
            {
                output.SuppressOutput();
                return;
            }

            if (!s_contentTypeProvider.TryGetContentType(Src, out var contentType))
            {
                contentType = "application/octet-stream";
            }

            output.TagName = "img";
            var srcAttribute = $"data:{contentType};base64,{fileContent}";

            output.Attributes.RemoveAll("src");
            output.Attributes.Add("src", srcAttribute);
            output.TagMode = TagMode.SelfClosing;
            output.Content.AppendHtml(fileContent);
        }
    }


}
