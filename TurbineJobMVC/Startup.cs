using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Arch.EntityFrameworkCore.UnitOfWork;
using AutoMapper;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using TurbineJobMVC.AutoMapperSettings;
using TurbineJobMVC.BuilderExtensions;
using TurbineJobMVC.Models;
using TurbineJobMVC.Services;
using TurbineJobMVC.Settings;

namespace TurbineJobMVC
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration,
            IHostEnvironment host)
        {
            Configuration = configuration;
            hostEnvironment = host;
        }

        public IConfiguration Configuration { get; }
        private IHostEnvironment hostEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDetection();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyHeader();
                    policy.AllowAnyMethod();
                    policy.AllowAnyOrigin();
                });
            });
            services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo(@"DataProtectionKeys/"))
                .SetApplicationName("TurbineJobMVC");
            services.AddAutoMapper(typeof(PCStockDBMappingProfiles));
            services
                .AddDbContext<PCStockDBContext>(options =>
                {
                    options.UseSqlServer(Configuration.GetConnectionString("PCStockDBConnectionString"));
                })
                .AddUnitOfWork<PCStockDBContext>();
            services.AddSession();
            services.AddResponseCaching();
            services.AddDNTCaptcha(options => options.UseCookieStorageProvider());
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });
            services.Configure<BrotliCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Optimal;
            });

            services.AddResponseCompression(action =>
            {
                action.EnableForHttps = true;
                action.Providers.Add<BrotliCompressionProvider>();
                action.Providers.Add<GzipCompressionProvider>();
            });
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.AddScoped<IWorkOrderService, WorkOrderService>();
            services.AddScoped<IDateTimeService, DateTimeService>();
            services.AddScoped<IService, Service>();
            services.AddScoped<IUserService, UserService>();
            services.AddHttpContextAccessor();
            services.AddControllersWithViews()
                .AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            var mimeTypeProvider = new FileExtensionContentTypeProvider();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseResponseCompression();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const int durationInSeconds = 60 * 60 * 24;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;

                    var headers = ctx.Context.Response.Headers;
                    var contentType = headers["Content-Type"];

                    if (contentType != "application/x-gzip" && !ctx.File.Name.EndsWith(".gz")) return;

                    var fileNameToTry = ctx.File.Name.Substring(0, ctx.File.Name.Length - 3);

                    if (!mimeTypeProvider.TryGetContentType(fileNameToTry, out var mimeType)) return;
                    headers.Add("Content-Encoding", "gzip");
                    headers["Content-Type"] = mimeType;
                }
            });
            app.UseSession();
            app.UseResponseCaching();
            app.UseStatusCodePagesWithRedirects("/Home/Error");
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCheckBrowserMiddleware();
            app.UseSitemapMiddleware();
            app.UseRobotsTxt(builder =>
                builder
                    .AddSection(section =>
                        section
                            .AddComment("Allow Googlebot")
                            .AddUserAgent("Googlebot")
                            .Allow("/")
                    )
                    .AddSection(section =>
                        section
                            .AddComment("Disallow the rest")
                            .AddUserAgent("*")
                            .AddCrawlDelay(TimeSpan.FromSeconds(10))
                            .Allow("/")
                    ));
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}