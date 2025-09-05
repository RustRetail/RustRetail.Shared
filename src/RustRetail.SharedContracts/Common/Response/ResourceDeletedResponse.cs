namespace RustRetail.SharedContracts.Common.Response
{
    /// <summary>
    /// Generic response for DELETE request.
    /// </summary>
    /// <param name="Id">Identifier of the deleted resource.</param>
    public record ResourceDeletedResponse(
        string Id);
}
