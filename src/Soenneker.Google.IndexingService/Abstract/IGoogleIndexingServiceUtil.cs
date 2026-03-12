using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Google.IndexingService.Abstract;

/// <summary>
/// An async thread-safe singleton for the Google indexing service client
/// </summary>
public interface IGoogleIndexingServiceUtil : IDisposable, IAsyncDisposable
{
    ValueTask<global::Google.Apis.Indexing.v3.IndexingService> Get(string fileName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Should be used if the component using is disposed (unless the entire app is being disposed).
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    ValueTask Remove(string fileName, CancellationToken cancellationToken = default);

    /// <inheritdoc cref="Remove(string, CancellationToken)"/>"/>
    void RemoveSync(string fileName, CancellationToken cancellationToken = default);
}