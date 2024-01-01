using System;
using System.Threading.Tasks;

namespace Soenneker.Google.IndexingService.Abstract;

/// <summary>
/// An async thread-safe singleton for the Google indexing service client
/// </summary>
public interface IGoogleIndexingServiceUtil : IDisposable, IAsyncDisposable
{
    ValueTask<global::Google.Apis.Indexing.v3.IndexingService> Get(string fileName);

    /// <summary>
    /// Should be used if the component using is disposed (unless the entire app is being disposed).
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    ValueTask Remove(string fileName);

    /// <inheritdoc cref="Remove(string)"/>"/>
    void RemoveSync(string fileName);
}