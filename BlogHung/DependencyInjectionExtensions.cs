using Serilog;
using BlogHung.Infrastructure.Configuration;
using BlogHung.Infrastructure.Utilities;
using BlogHung.Infrastructure.Logging;
using BlogHung.Infrastructure.Database;

namespace BlogHung
{
    public static class DependencyInjectionExtensions
    {


        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddHttpClientServicesCallApi(this IServiceCollection services)
        {
            services.AddSingleton<IMongoDbContext, MongoDbContext>();
            services.AddSingleton<IQuery, SqlServer>();
            /*services.AddTransient<ChatClientClientDelegatingHandler>();

            services.AddHttpClient(HttpClientName.ChatServer, client =>
            {
                client.BaseAddress = new Uri("http://14.225.192.203/");
                client.Timeout = TimeSpan.FromSeconds(20);
            }).AddHttpMessageHandler<ChatClientClientDelegatingHandler>();*/

            return services;
        }

        public static IServiceCollection AddInfrastructureLayer(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddSingleton<ICoreHttpClient, CoreHttpClient>();
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));
            services.AddHttpClientServices();
            return services;
        }

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
            CoreUtility.ConfigureContextAccessor(app.ApplicationServices.GetRequiredService<IHttpContextAccessor>());
            LoggingHelper.Config(app.ApplicationServices.GetRequiredService<IDiagnosticContext>());
            CoreUtility.ConfigureHttpClientFactory(app.ApplicationServices.GetRequiredService<IHttpClientFactory>());

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
