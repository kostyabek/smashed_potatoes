using CourseWork.Core.Helpers;
using CourseWork.Core.Quartz;
using CourseWork.Core.Quartz.Jobs;
using CourseWork.Core.Services.WeeklySummaryEmailService;
using CourseWork.Web.Extensions;
using CourseWork.Web.Middlewares;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Quartz.Spi;

namespace CourseWork.Web
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddFluentValidation(c =>
            {
                c.RegisterValidatorsFromAssemblyContaining<Startup>();
            });
            services.AddRazorPages();
            services.AddCustomServices();
            services.AddSwagger();
            services.AddIdentity();
            services.AddDataAccess(Configuration);
            services.AddMediatr();
            services.AddConfigurations(Configuration);
            services.AddOpenIdConnectAuthentication(Configuration);
            services.AddAuthorization();
            services.AddCors(o => o.AddPolicy("SmashedPotatoesPolicy", b =>
            {
                b.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));

            services.AddTransient<IJobFactory, QuartzJobFactory>();
            services.AddScoped<WeeklySummaryEmailJob>();
            services.AddScoped<IWeeklySummaryEmailService, WeeklySummaryEmailService>();
        }

        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(StoragePathsHelper.GetImagesPath()),
                RequestPath = "/images"
            });

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

            app.UseRouting();

            app.UseCors("SmashedPotatoesPolicy");

            app.UseAuthentication();
            app.UseMiddleware<BanMiddleware>();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
