namespace RustRetail.SharedContracts.Common.Response
{
    /// <summary>
    /// Generic response for PUT/PATCH/Update request.
    /// </summary>
    /// <param name="Location">The location/URL of the modified resource.</param>
    public record ResourceModifiedResponse(
        string Location);
}
