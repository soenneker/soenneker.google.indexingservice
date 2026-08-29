using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Google.IndexingService.Abstract;

/// <summary>
/// An async thread-safe singleton for the Google indexing service client
/// </summary>
public interface IGoogleIndexingServiceUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured global::Google.Apis.Indexing.v3.Indexing Service used by the google indexing service.
    /// </summary>
    /// <param name="fileName">Name of the target file.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested global::Google.Apis.Indexing.v3.Indexing Service.</returns>
    ValueTask<global::Google.Apis.Indexing.v3.IndexingService> Get(string fileName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes and disposes the cached indexing client associated with a credential file.
    /// </summary>
    /// <param name="fileName">Credential filename used as the cache key.</param>
    /// <param name="cancellationToken">Token used to cancel asynchronous disposal.</param>
    /// <returns>A task whose result is <see langword="true"/> when a cached client was removed; otherwise, <see langword="false"/>.</returns>
    ValueTask<bool> Remove(string fileName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes sync.
    /// </summary>
    /// <param name="fileName">The file name.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    void RemoveSync(string fileName, CancellationToken cancellationToken = default);
}
