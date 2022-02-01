using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
//using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp.PixelFormats;

namespace Aircon.Business.Avatar
{
    public interface IAvatarService
    {
        Task<byte[]> GenerateAvatar(string name, string formatExtension, Int32 squareSize, CancellationToken cancellationToken = default);
        Task<byte[]> GenerateAvatar(string name, Int32 squareSize, CancellationToken cancellationToken = default);
    }
    public class AvatarService : IAvatarService
    {
        private readonly IEnumerable<IAvatarGenerator> _avatarGenerators;
        private readonly IAvatarGenerator _avatarGenerator;

        private readonly IPaletteProvider _paletteProvider;
        private readonly ILogger<AvatarService> _log;

        public AvatarService(IEnumerable<IAvatarGenerator> avatarGenerators,
                             IPaletteProvider paletteProvider,
                             ILogger<AvatarService> log)
        {
            _avatarGenerators = avatarGenerators;
            _paletteProvider = paletteProvider;
            _log = log;
        }

        public async Task<byte[]> GenerateAvatar(string name, string formatExtension, Int32 squareSize, CancellationToken cancellationToken)
        {
            name = AvatarHelpers.CleanName(name);

            var backgroundColor = await _paletteProvider.GetColorForString(name, cancellationToken);

            var generator = _avatarGenerators.FirstOrDefault(p => p.Extension.Equals(formatExtension, StringComparison.OrdinalIgnoreCase));
            if (generator == null)
                throw new InvalidOperationException("No generator found for extension " + formatExtension);

            var buffer = await generator.GenerateAvatar(name, squareSize, Rgba32.ParseHex("fff"), backgroundColor, cancellationToken);
            return buffer;
        }

        //public AvatarService(IAvatarGenerator avatarGenerator,
        //                     IPaletteProvider paletteProvider
        //                     )
        //{
        //    _avatarGenerator = avatarGenerator;
        //    _paletteProvider = paletteProvider;
        //}

        public async Task<byte[]> GenerateAvatar(string name, Int32 squareSize, CancellationToken cancellationToken)
        {
            name = AvatarHelpers.CleanName(name);
            var backgroundColor = await _paletteProvider.GetColorForString(name, cancellationToken);
            var buffer = await _avatarGenerator.GenerateAvatar(name, squareSize, Rgba32.ParseHex("fff"), backgroundColor, cancellationToken);
            return buffer;
        }

    }
}
