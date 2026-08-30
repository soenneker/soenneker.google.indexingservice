using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Google.IndexingService.Abstract;

/// <summary>
/// Provides lazily initialized Google Indexing API clients keyed by service-account credential filename.
/// </summary>
public interface IGoogleIndexingServiceUtil : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets or creates the authenticated Indexing API client for a credential file.
    /// </summary>
    /// <param name="fileName">The credential filename relative to <c>LocalResources</c>.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>The cached client associated with <paramref name="fileName"/>.</returns>
    ValueTask<global::Google.Apis.Indexing.v3.IndexingService> Get(string fileName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Removes and disposes the cached indexing client associated with a credential file.
    /// </summary>
    /// <param name="fileName">Credential filename used as the cache key.</param>
    /// <param name="cancellationToken">Token used to cancel asynchronous disposal.</param>
    /// <returns>A task whose result is <see langword="true"/> when a cached client was removed; otherwise, <see langword="false"/>.</returns>
    ValueTask<bool> Remove(string fileName, CancellationToken cancellationToken = default);

    /// <summary>
    /// Synchronously removes and disposes the cached indexing client associated with a credential file.
    /// </summary>
    /// <param name="fileName">Credential filename used as the cache key.</param>
    /// <param name="cancellationToken">Token observed while removing the client.</param>
    void RemoveSync(string fileName, CancellationToken cancellationToken = default);
}
