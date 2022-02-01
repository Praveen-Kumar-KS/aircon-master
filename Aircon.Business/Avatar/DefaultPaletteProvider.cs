using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SixLabors.ImageSharp.PixelFormats;

namespace Aircon.Business.Avatar
{

    public interface IPaletteProvider
    {
        Task<Rgba32[]> GetPalette(CancellationToken cancellationToken = default(CancellationToken));

        Task<Rgba32> GetColorForString(string input, CancellationToken cancellationToken = default);
    }

    public abstract class PaletteProviderBase : IPaletteProvider
    {
        public abstract Task<Rgba32[]> GetPalette(CancellationToken cancellationToken);

        public virtual async Task<Rgba32> GetColorForString(string input, CancellationToken cancellationToken)
        {
            using (var md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                var value = Math.Abs(BitConverter.ToInt32(data, 0));
                var palette = await GetPalette(cancellationToken);

                return palette[value % palette.Length];
            }
        }
    }

    public class DefaultPaletteProvider : PaletteProviderBase
    {
        private readonly Rgba32[] _palette;

        public DefaultPaletteProvider()
        {
            _palette = new[] {
                new Rgba32(235,95,0),
                new Rgba32(250,117,27),
                new Rgba32(254,102,0),
                new Rgba32(82,181,255),
                new Rgba32(255,161,98),
                new Rgba32(255,133,51),
                new Rgba32(245,98,0),
                new Rgba32(0,94,162),
                new Rgba32(82,181,255),
            };
        }

        public DefaultPaletteProvider(Rgba32[] palette)
        {
            _palette = palette ?? throw new ArgumentNullException(nameof(palette));
        }

        public override Task<Rgba32[]> GetPalette(CancellationToken cancellationToken) => Task.FromResult(_palette);
    }

}
