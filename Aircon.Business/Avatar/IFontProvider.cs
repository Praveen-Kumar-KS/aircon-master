using SixLabors.Fonts;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Aircon.Business.Avatar
{
    public interface IFontProvider
    {
        Task<FontFamily> GetFont(CancellationToken cancellationToken = default);
    }

    public class DefaultFontProvider : IFontProvider
    {
        private readonly FontCollection _fontCollection;
        private readonly string _fontName;

        public DefaultFontProvider()
        {
            _fontCollection = new FontCollection();
            _fontName = "Roboto";

            var assembly = typeof(DefaultFontProvider).GetTypeInfo().Assembly;
            using (var stream = assembly.GetManifestResourceStream("Aircon.Business.Avatar.Resources.Roboto-Regular.ttf"))
                _ = _fontCollection.Install(stream);
        }

        public DefaultFontProvider(string fontName, Stream fontData)
        {
            _fontCollection = new FontCollection();
            _fontName = fontName;
            _ = _fontCollection.Install(fontData);
        }

        public Task<FontFamily> GetFont(CancellationToken cancellationToken)
        {
            if (_fontCollection.TryFind(_fontName, out var fontFamily))
                return Task.FromResult(fontFamily);

            throw new Exception($"Font {_fontName} not found.");
        }
    }

}
