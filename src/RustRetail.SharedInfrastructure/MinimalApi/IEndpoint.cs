using Microsoft.AspNetCore.Routing;

namespace RustRetail.SharedInfrastructure.MinimalApi
{
    public interface IEndpoint
    {
        void MapEndpoint(IEndpointRouteBuilder app);
    }
}
