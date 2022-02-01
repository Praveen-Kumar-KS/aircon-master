using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using System.IO;

namespace Aircon.Business.Avatar
{
    public static class AvatarExtensions
    {

        public static IImageProcessingContext ConvertToAvatar(this IImageProcessingContext processingContext, Size size, float cornerRadius)
        {
            return processingContext.Resize(new ResizeOptions
            {
                Size = size,
                Mode = ResizeMode.Crop
            });
        }

        public static IApplicationBuilder UseAvatars(this IApplicationBuilder builder, string endpoint)
        {
            return builder.UseMiddleware<AvatarMiddleware>(new PathString(endpoint));
        }

        public static IServiceCollection AddAvatarPalette(this IServiceCollection services)
        {
            services.AddSingleton<IPaletteProvider>(new DefaultPaletteProvider());
            return services;
        }

        public static IServiceCollection AddAvatarPalette(this IServiceCollection services, Rgba32[] palette)
        {
            services.AddSingleton<IPaletteProvider>(new DefaultPaletteProvider(palette));
            return services;
        }

        public static IServiceCollection AddAvatarFont(this IServiceCollection services)
        {
            services.AddSingleton<IFontProvider>(new DefaultFontProvider());
            return services;
        }

        public static IServiceCollection AddAvatarFont(this IServiceCollection services, string fontName, Stream fontData)
        {
            services.AddSingleton<IFontProvider>(new DefaultFontProvider(fontName, fontData));
            return services;
        }

        public static IServiceCollection AddAvatarGenerators(this IServiceCollection services)
        {
            services.AddSingleton<IAvatarGenerator, SvgAvatarGenerator>();
            return services;
        }

        public static IServiceCollection AddAvatarService(this IServiceCollection services)
        {
            services.AddSingleton<IAvatarService, AvatarService>();
            return services;
        }

        public static IServiceCollection AddAvatar(this IServiceCollection services)
        {
            services.AddScoped<IAvatarUserService, AvatarUserService>();
            services
                .AddAvatarService()
                //.AddAvatarFactories()
                .AddAvatarFont()
                .AddAvatarPalette()
                .AddAvatarGenerators();
            return services;
        }

        public static string ToRgbHex(this Rgba32 color)
        {
            var hex = color.ToHex();
            return hex.Substring(0, 6);
        }
    }
}
