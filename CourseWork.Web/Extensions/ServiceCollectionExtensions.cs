using System;
using System.IO;
using System.Reflection;
using CourseWork.Application;
using CourseWork.Domain.Identity;
using CourseWork.Infrastructure.Database;
using MediatR;
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
                .AddEntityFrameworkStores<BaseDbContext>();

            services.Configure<IdentityOptions>(o =>
            {
                o.User.RequireUniqueEmail = true;

                o.Password.RequiredUniqueChars = 5;

                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(60);
                o.Lockout.MaxFailedAccessAttempts = 7;

                o.SignIn.RequireConfirmedEmail = true;
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
        /// Adds the application authentication.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddAppAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(s => s.DefaultAuthenticateScheme = "")
                .AddCookie(o =>
                {

                });

            return services;
        }
    }
}
