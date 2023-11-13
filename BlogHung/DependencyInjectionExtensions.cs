using Serilog;
using BlogHung.Infrastructure.Configuration;
using BlogHung.Infrastructure.Utilities;
using BlogHung.Infrastructure.Logging;

namespace BlogHung
{
    public static class DependencyInjectionExtensions
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static IServiceCollection AddHttpClientServices(this IServiceCollection services)
        {
            services.AddHttpClient();

            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddCoreInfrastructureLayer(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ApplicationServices.CreateLoggerConfiguration(env);
            AppSettingServices.Services = app.ApplicationServices;
            CoreUtility.ConfigureContextAccessor(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            LoggingHelper.Config(app.ApplicationServices.GetRequiredService<IDiagnosticContext>());
            //CoreUtility.ConfigureHttpClientFactory(app.ApplicationServices.GetRequiredService<IHttpClientFactory>());

            app.UseSerilogRequestLogging(opts =>
            {
                opts.EnrichDiagnosticContext = DiagnosticContext.EnrichFromRequest;
            });

            return app;
        }

        public static IApplicationBuilder AddInfrastructureLayer(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            AppSettingServices.Services = app.ApplicationServices;
            Helper.ServiceProvider = app.ApplicationServices;
            return app;
        }
    }
}
