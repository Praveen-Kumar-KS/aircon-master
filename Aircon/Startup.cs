using System;
using System.Linq;
using System.Security.Claims;
using AspNet.Security.OAuth.Validation;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Aircon.Data;
using Aircon.Data.Entities;
using Aircon.Helper;
using Aircon.Business.Services;
using AutoMapper;
using Aircon.Business.Services.Shared;
using Aircon.Business.Services.Lookups;
using Aircon.Providers;
using Aircon.FileUpload.Service;
using Aircon.Business.Helper;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using Aircon.Data.Security;
using Aircon.Core;
using Aircon.Data.Helper;
using Aircon.Business.Services.Security;
using Aircon.Core.Caching;
using Aircon.Framework;
using Microsoft.AspNetCore.ResponseCompression;
using Aircon.Business.Security;
using Aircon.Business.Services.Customer;
using Aircon.Business.Services.Admin;
using Aircon.Business.Services.Identity;
using Aircon.Business.Services.SystemAdmin;
using Aircon.Business.Avatar;
using Aircon.Core.Configuration;
using Aircon.Core.Infrastructure;
using Aircon.Extensions;
using Aircon.Business.Media;
using Aircon.Business.Extensions;
using Newtonsoft.Json.Serialization;
using Vg.Common.Notification.Message;
using Vg.Common.Notification;
using Vg.Common.Notification.Extensions;
using Vg.Common.ToastNotify.Components;
using Vg.Common.ToastNotify;

namespace Aircon
{
    public class Startup
    {
        public IHostEnvironment HostingEnvironment { get; }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfigurationRoot Configuration { get; }

        private readonly IConfiguration Config;
        public Startup(IHostEnvironment env, IConfiguration config)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            //builder.AddEnvironmentVariables();
            Configuration = builder.Build();
            HostingEnvironment = env;
            Config = config;

        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            //services.AddKendo();
            //services.AddMemoryCache();
            services.AddMvc(config =>
            {
                //var policy = new AuthorizationPolicyBuilder()
                //                 .RequireAuthenticatedUser()
                //                 .Build();
                //config.Filters.Add(new AuthorizeFilter(policy));
                config.Conventions.Add(new LookupRouteConvention());
                config.Conventions.Add(new NoteRouteConvention());
                config.Conventions.Add(new AttachmentEntityRouteConvention());
            })//.ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new LookupControllerProvider()))
              .ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new AttachmentEntityControllerProvider()))
              .ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new NoteControllerProvider()))
              .AddJsonOptions(options =>
              {
                  options.JsonSerializerOptions.IgnoreNullValues = true;
                  options.JsonSerializerOptions.PropertyNamingPolicy = null;
                  options.JsonSerializerOptions.DictionaryKeyPolicy = null;
              })
              .AddApplicationPart(typeof(ToastNotifyViewComponent).Assembly)
              .AddToastNotifyToastr(new ToastrOptions()
              {
                  ProgressBar = false,
                  PositionClass = ToastPositions.TopRight
              });
            services.AddResponseCompression(options => {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
                options.EnableForHttps = true;
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });

            ConfigureDatabaseServices(services);
            var cahingSection = Config.GetSection("Caching");
            CommonHelper.CacheTimeMinutes = int.Parse(cahingSection["DefaultCacheTimeMinutes"]);
            services.AddSingleton<IAuthenticationSchemeProvider, CustomAuthenticationSchemeProvider>();

            services.AddIdentity<User, Role>()
                    .AddUserManager<UserManager<User>>()
                    .AddEntityFrameworkStores<AirconDbContext>()
                    .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {

                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
                
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                options.SignIn.RequireConfirmedAccount = true;

                options.ClaimsIdentity.UserNameClaimType = ClaimTypes.Name;
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier;
                options.ClaimsIdentity.RoleClaimType = ClaimTypes.Role;
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = OAuthValidationDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var key = Encoding.ASCII.GetBytes(Config["ApiSecret"]);
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                };
            });
            services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNewtonsoftJson(); 
            services.AddRazorPages().AddNewtonsoftJson(); ;

            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                builder =>
                {
                    builder.WithOrigins("https://localhost:44396")
                                        .AllowAnyHeader()
                                        .AllowAnyMethod();
                });
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });

            services.AddAutoMapper(typeof(Startup));
            //services.AddTransient<IEmailSender, AuthMessageSender>();
            services.AddTransient<ISmsSender, AuthMessageSender>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IConfigValueProvider, AppConfigValueProvider>();
            services.AddScoped<IAzureStorageService, AzureStorageService>();
            services.AddScoped<IStoredFileService, StoredFileService>();
            services.AddScoped<IUserClaimsPrincipalFactory<User>, AirconUserClaimsPrincipalFactory>();
            services.AddScoped<ISignUpService, SignUpService>();
            services.AddScoped<IPreferenceService, PreferenceService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IUserProfileService, UserProfileService>();
            services.AddScoped<ICustomerUserService, CustomerUserService>();
            services.AddScoped<ICustomerAdminService, CustomerAdminService>();
            services.AddScoped<ICompanyService, CompanyService>();
            services.AddScoped<ICustomerContactService, CustomerContactService>();
            services.AddScoped<IEmployeeUserService, EmployeeUserService>();
            services.AddScoped<IPermissionMappingService, PermissionMappingService>();
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IAdminCommonService, AdminCommonService>();
            services.AddScoped<IGenericAttributeService, GenericAttributeService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IAirFileProvider, AirFileProvider>();
            services.AddScoped<IQuotesService, QuotesService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped(typeof(INoteEntityService<>), typeof(NoteEntityService<>));
            var appSettings = new AppSettings();
            services.AddSingleton<AppSettings>();

            Singleton<AppSettings>.Instance = appSettings;

            RegisterCache(services);

            services.AddSeed();

            services.Configure<SmtpEmailSenderConfiguration>(options => Configuration.GetSection("MailKit").Bind(options));

            services.AddScoped<IPermissionProvider, StandardPermissionProvider>();
            //services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<IWebHelper, WebHelper>();
            services.AddScoped<IWorkContext, WebWorkContext>();
            services.AddScoped(typeof(ILookupService<>), typeof(LookupService<>));
            services.AddScoped(typeof(IAttachmentEntityService<>), typeof(AttachmentEntityService<>));
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<HostingConfig>();
            services.AddScoped<ITemplateDefinitionService, TemplateDefinitionService>();
            services.AddEmailJob();

            //services.Configure<BackgroundJobOptions>(options =>
            //{
            //    options.AddJob<BackgroundEmailSendingJob>();
            //});

            services.AddAvatar();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public virtual void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory, IHostEnvironment environment)
        {
            var envName = environment.EnvironmentName;

            app.UseToastNotify();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:9000");
                builder.WithMethods("GET");
                builder.WithHeaders("Authorization");
                builder.WithOrigins("http://localhost:56495");
                builder.WithOrigins("https://localhost:44396");
            });
            app.UseStaticFiles();

            app.UseWhen(context => context.Request.Path.StartsWithSegments("/api"), branch =>
            {
                //branch.UseOAuthValidation();
                branch.UseAuthentication();

            });

            app.UseWhen(context => !context.Request.Path.StartsWithSegments("/api"), branch =>
            {
                branch.UseStatusCodePagesWithReExecute("/error/{0}");
                app.UseAuthentication();
                //branch.UseIdentity();
            });


            app.UseAvatars("/avatars");
            app.UseImages("/i");
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseSerilogRequestLogging();
            app.UseRouting();
            app.UseAuthorization();
            app.UseStateAutoMapper();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapAreaControllerRoute(
                    name: "AdminArea",
                    areaName: "Admin",
                    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "IdentityArea",
                    areaName: "Identity",
                    pattern: "Identity/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "CustomerArea",
                    areaName: "Customer",
                    pattern: "Customer/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapAreaControllerRoute(
                    name: "SystemAdminArea",
                    areaName: "SystemAdmin",
                    pattern: "SystemAdmin/{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            //app.Run(async (context) =>
            //{
            //    var logger = loggerFactory.CreateLogger("TodoApi.Startup");
            //    logger.LogInformation("No endpoint found for request {path}", context.Request.Path);
            //    await context.Response.WriteAsync("No endpoint found - try /api/todo.");
            //});
            //InitKeyManagement();
            HttpContextHelper.Configure(app.ApplicationServices.
                GetRequiredService<IHttpContextAccessor>()
            );
           // app.UseMiddleware<WorkContextMiddleware>();
        }
        private void InitKeyManagement()
        {
            Paths.EnsureNecessaryDirectoriesArePresent();
            if (null == KeyfileHelper.ReadFileEncryptionKey())
            {
                KeyfileHelper.GenerateAndPersistFileEncryptionKey();
            }
        }

        private void RegisterCache(IServiceCollection services)
        {
            services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
            //if (config.RedisPubSubEnabled)
            //{
            //    var redis = ConnectionMultiplexer.Connect(config.RedisPubSubConnectionString);
            //    builder.Register(c => redis.GetSubscriber()).As<ISubscriber>().SingleInstance();
            //    builder.RegisterType<RedisMessageBus>().As<IMessageBus>().SingleInstance();
            //    builder.RegisterType<RedisMessageCacheManager>().As<ICacheManager>().SingleInstance();
            //}
        }

        protected virtual void ConfigureDatabaseServices(IServiceCollection services)
        {

            if (HostingEnvironment.IsEnvironment("Testing"))
            {
                Console.WriteLine("Testing");
            }
            else
            {
                var connectionString = Config.GetConnectionString("DefaultConnection");
                services.AddDbContext<AirconDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            }


        }


    }
}
