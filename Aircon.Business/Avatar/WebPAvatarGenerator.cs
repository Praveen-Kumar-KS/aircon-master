using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Shorthand.ImageSharp.WebP;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace Aircon.Business.Avatar
{
    public class WebPAvatarGenerator : ImageAvatarGeneratorBase
    {
        public WebPAvatarGenerator(IFontProvider fontProvider)
            : base(fontProvider) { }

        public override string Extension => "webp";

        public override string MimeType => "image/webp";

        protected override Task<byte[]> RenderGlyphs(IPathCollection glyphs, Int32 squareSize, Rgba32 foregroundColor, Rgba32 backgroundColor, CancellationToken cancellationToken)
        {
            using (var img = new Image<Rgba32>(squareSize, squareSize))
            {
                var graphicsOptions = new ShapeGraphicsOptions();
                var brush = new SolidBrush(foregroundColor);
                img.Mutate(ctx => ctx
                    .Fill(backgroundColor)
                    .Fill(graphicsOptions, brush, glyphs));

 //               img.Clone(x => x.ConvertToAvatar(new Size(squareSize, squareSize), 100));

                //using (var ms = new MemoryStream())
                //{
                //    using (Image<Rgba32> destRound = img.Clone(x => x.ConvertToAvatar(new Size(200, 200), 100)))
                //    {
                //        destRound.SaveAsWebP(ms);
                //        ms.Seek(0, SeekOrigin.Begin);
                //        return Task.FromResult(ms.ToArray());
                //    }
                //}

                using (var ms = new MemoryStream())
                {
                    img.SaveAsWebP(ms);
                    ms.Seek(0, SeekOrigin.Begin);
                    return Task.FromResult(ms.ToArray());
                }

            }
        }


    }
}
