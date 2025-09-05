namespace RustRetail.SharedContracts.Common.Response
{
    /// <summary>
    /// Generic response for POST/Create request.
    /// </summary>
    /// <param name="Location">The location/URL of the created resource.</param>
    public record ResourceCreatedResponse(
        string Location);
}
