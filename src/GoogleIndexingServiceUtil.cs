using Google.Apis.Services;
using Soenneker.Google.Credentials.Abstract;
using Soenneker.Google.IndexingService.Abstract;
using Soenneker.Utils.SingletonDictionary;
using System;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Soenneker.Extensions.ValueTask;

namespace Soenneker.Google.IndexingService;

/// <inheritdoc cref="IGoogleIndexingServiceUtil"/>
public sealed class GoogleIndexingServiceUtil: IGoogleIndexingServiceUtil
{
    private readonly SingletonDictionary<global::Google.Apis.Indexing.v3.IndexingService> _indexingServices;

    public GoogleIndexingServiceUtil(IGoogleCredentialsUtil googleCredentialsUtil)
    {
        _indexingServices = new SingletonDictionary<global::Google.Apis.Indexing.v3.IndexingService>(async (filename, token, args) =>
        {
            string[] scopes = ["https://www.googleapis.com/auth/indexing"];
            ICredential credential = await googleCredentialsUtil.Get(filename, scopes, token).NoSync();

            return new global::Google.Apis.Indexing.v3.IndexingService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
        });
    }

    public ValueTask<global::Google.Apis.Indexing.v3.IndexingService> Get(string fileName, CancellationToken cancellationToken = default)
    {
        return _indexingServices.Get(fileName, cancellationToken, fileName);
    }

    public ValueTask Remove(string fileName, CancellationToken cancellationToken = default)
    {
        return _indexingServices.Remove(fileName, cancellationToken);
    }

    public void RemoveSync(string fileName, CancellationToken cancellationToken = default)
    {
        _indexingServices.RemoveSync(fileName, cancellationToken);
    }

    public ValueTask DisposeAsync()
    {
        GC.SuppressFinalize(this);

        return _indexingServices.DisposeAsync();
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);

        _indexingServices.Dispose();
    }
}
