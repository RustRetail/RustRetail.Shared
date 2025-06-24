using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace RustRetail.SharedInfrastructure.Logging.Serilog
{
    /// <summary>
    /// Provides extension methods for configuring Serilog logging in a host builder and application pipeline.
    /// </summary>
    public static class SerilogLoggingExtensions
    {
        /// <summary>
        /// Configures the host builder to use Serilog with settings from the application's configuration.
        /// </summary>
        /// <param name="hostBuilder">The host builder to configure.</param>
        /// <returns>The configured <see cref="IHostBuilder"/> instance.</returns>
        public static IHostBuilder UseSharedSerilog(this IHostBuilder hostBuilder)
        {
            hostBuilder.UseSerilog((context, configuration) =>
            {
                configuration.ReadFrom.Configuration(context.Configuration);
            });

            return hostBuilder;
        }

        /// <summary>
        /// Adds Serilog request logging middleware to the application's request pipeline.
        /// </summary>
        /// <param name="app">The application builder to configure.</param>
        /// <returns>The configured <see cref="IApplicationBuilder"/> instance.</returns>
        public static IApplicationBuilder UseSharedSerilogRequestLogging(this IApplicationBuilder app)
        {
            // Can add additional configuration here if needed
            app.UseSerilogRequestLogging();

            return app;
        }
    }
}
