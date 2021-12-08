using System;
using System.IO;
using System.Reflection;
using CourseWork.Common.Configurations;
using CourseWork.Core;
using CourseWork.Core.Database;
using CourseWork.Core.Database.Entities.Identity;
using CourseWork.Core.Helpers.DatabaseConnectionHelper;
using CourseWork.Core.Helpers.EmailConfirmationHelper;
using CourseWork.Core.Helpers.EmailTemplateHelper;
using CourseWork.Core.Services.UserService;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace CourseWork.Web.Extensions
{
    /// <summary>
    /// Contains extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds the ASP.NET identity provider.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>()
                .AddDefaultTokenProviders()
                .AddEntityFrameworkStores<BaseDbContext>();

            services.Configure<IdentityOptions>(o =>
            {
                o.User.RequireUniqueEmail = true;

                o.Password.RequiredUniqueChars = 5;

                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                o.Lockout.MaxFailedAccessAttempts = 7;

                o.SignIn.RequireConfirmedEmail = false;
            });

            return services;
        }

        /// <summary>
        /// Adds the data access.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSql");

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<BaseDbContext>(o => o.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly("CourseWork.Web")))
                .AddDbContextFactory<BaseDbContext>(
                    b => b.UseNpgsql(connectionString),
                    ServiceLifetime.Scoped);

            return services;
        }

        /// <summary>
        /// Adds the swagger.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Smashed Potatoes",
                    Description = "Some sort of a imageboard/forum API",
                    Contact = new OpenApiContact
                    {
                        Name = "Kostiantyn Biektin",
                    },
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        /// <summary>
        /// Adds the mediatr.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddMediatr(this IServiceCollection services)
        {
            services.AddMediatR(typeof(MediatrEntryPoint).Assembly);

            return services;
        }

        /// <summary>
        /// Adds the configurations.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<SmtpClientCredentials>(configuration.GetSection("SmtpClientCredentials"));
            services.Configure<HostingSettings>(configuration.GetSection("HostingSettings"));
            return services;
        }

        /// <summary>
        /// Adds the application authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="configuration">The configuration.</param>
        /// <returns></returns>
        public static IServiceCollection AddOpenIdConnectAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            /*services.AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                    o.DefaultForbidScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = GoogleOpenIdConnectDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddGoogleOpenIdConnect(o =>
                {
                    o.ClientId = configuration["GoogleAuthCredentials:ClientId"];
                    o.ClientSecret = configuration["GoogleAuthCredentials:ClientSecret"];
                    o.CallbackPath = new PathString("/signin-google");
                });*/

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
                {
                });

            return services;
        }

        /// <summary>
        /// Adds the custom services.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IDatabaseConnectionHelper, DatabaseConnectionHelper>();
            services.AddScoped<IEmailTemplateHelper, EmailTemplateHelper>();
            services.AddScoped<IEmailConfirmationHelper, EmailConfirmationHelper>();

            return services;
        }
    }
}
