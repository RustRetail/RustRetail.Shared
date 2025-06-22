using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;

namespace RustRetail.SharedInfrastructure.MinimalApi
{
    /// <summary>
    /// Provides extension methods for registering and mapping minimal API endpoints.
    /// </summary>
    public static class EndpointExtensions
    {
        /// <summary>
        /// Registers all non-abstract, non-interface types implementing <see cref="IEndpoint"/> from the specified assembly as transient services.
        /// </summary>
        /// <param name="services">The service collection to add the endpoints to.</param>
        /// <param name="assembly">The assembly to scan for <see cref="IEndpoint"/> implementations.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddEndpoints(
            this IServiceCollection services,
            Assembly assembly)
        {
            ServiceDescriptor[] serviceDescriptors = assembly
                .DefinedTypes
                .Where(type => type is { IsAbstract: false, IsInterface: false } &&
                       type.IsAssignableTo(typeof(IEndpoint)))
                .Select(type => ServiceDescriptor.Transient(typeof(IEndpoint), type))
                .ToArray();

            services.TryAddEnumerable(serviceDescriptors);

            return services;
        }

        /// <summary>
        /// Maps all registered <see cref="IEndpoint"/> instances to the application's endpoint route builder.
        /// </summary>
        /// <param name="app">The web application to map endpoints for.</param>
        /// <param name="routeGroupBuilder">
        /// An optional <see cref="RouteGroupBuilder"/> to group endpoints; if <c>null</c>, endpoints are mapped directly to the application.
        /// </param>
        /// <returns>The <see cref="IApplicationBuilder"/> for chaining.</returns>
        public static IApplicationBuilder MapEndpoints(
            this WebApplication app,
            RouteGroupBuilder? routeGroupBuilder = null)
        {
            IEnumerable<IEndpoint> endpoints = app.Services.GetRequiredService<IEnumerable<IEndpoint>>();

            IEndpointRouteBuilder builder = routeGroupBuilder is null ? app : routeGroupBuilder;

            foreach (IEndpoint endpoint in endpoints)
            {
                endpoint.MapEndpoint(builder);
            }

            return app;
        }
    }
}
