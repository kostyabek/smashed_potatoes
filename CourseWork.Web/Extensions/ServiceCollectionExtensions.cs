namespace CourseWork.Web.Extensions
{
    using System;
    using System.IO;
    using System.Reflection;
    using Infrastructure.Database;
    using Infrastructure.Database.Identity;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    public static class ServiceCollectionExtensions
    {
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

        public static IServiceCollection AddDataAccess(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("PostgreSql");

            services.AddEntityFrameworkNpgsql()
                .AddDbContext<BaseDbContext>(o => o.UseNpgsql(connectionString,
                    b => b.MigrationsAssembly("CourseWork.Web")))
                .AddDbContextFactory<BaseDbContext>(b => b.UseNpgsql(connectionString),
                    ServiceLifetime.Scoped);

            return services;
        }

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
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                o.IncludeXmlComments(xmlPath);
            });

            return services;
        }
    }
}
