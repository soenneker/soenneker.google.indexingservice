using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Soenneker.Extensions.ValueTask;
using Soenneker.Google.Credentials.Abstract;
using Soenneker.Google.IndexingService.Abstract;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Dictionaries.SingletonKeys;

namespace Soenneker.Google.IndexingService;

/// <inheritdoc cref="IGoogleIndexingServiceUtil"/>
public sealed class GoogleIndexingServiceUtil : IGoogleIndexingServiceUtil
{
    private static readonly string[] _scopes = ["https://www.googleapis.com/auth/indexing"];

    private readonly IGoogleCredentialsUtil _googleCredentialsUtil;
    private readonly SingletonKeyDictionary<string, global::Google.Apis.Indexing.v3.IndexingService> _indexingServices;

    public GoogleIndexingServiceUtil(IGoogleCredentialsUtil googleCredentialsUtil)
    {
        _googleCredentialsUtil = googleCredentialsUtil;

        // method group -> no closure capture
        _indexingServices = new SingletonKeyDictionary<string, global::Google.Apis.Indexing.v3.IndexingService>(CreateIndexingService);
    }

    private async ValueTask<global::Google.Apis.Indexing.v3.IndexingService> CreateIndexingService(string filename, CancellationToken token)
    {
        ICredential credential = await _googleCredentialsUtil.Get(filename, _scopes, token)
                                                             .NoSync();

        return new global::Google.Apis.Indexing.v3.IndexingService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential
        });
    }

    public ValueTask<global::Google.Apis.Indexing.v3.IndexingService> Get(string fileName, CancellationToken cancellationToken = default) =>
        _indexingServices.Get(fileName, cancellationToken);

    public ValueTask Remove(string fileName, CancellationToken cancellationToken = default) => _indexingServices.Remove(fileName, cancellationToken);

    public void RemoveSync(string fileName, CancellationToken cancellationToken = default) => _indexingServices.RemoveSync(fileName, cancellationToken);

    public void Dispose() => _indexingServices.Dispose();

    public ValueTask DisposeAsync() => _indexingServices.DisposeAsync();
}