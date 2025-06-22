using Microsoft.AspNetCore.Routing;

namespace RustRetail.SharedInfrastructure.MinimalApi
{
    /// <summary>
    /// Defines a contract for mapping minimal API endpoints to an endpoint route builder.
    /// </summary>
    public interface IEndpoint
    {
        /// <summary>
        /// Maps the endpoint to the specified <see cref="IEndpointRouteBuilder"/>.
        /// </summary>
        /// <param name="app">The endpoint route builder to which the endpoint will be mapped.</param>
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
