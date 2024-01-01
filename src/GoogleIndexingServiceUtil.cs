using Google.Apis.Services;
using Soenneker.Google.Credentials.Abstract;
using Soenneker.Google.IndexingService.Abstract;
using Soenneker.Utils.SingletonDictionary;
using System;
using System.Linq;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;

namespace Soenneker.Google.IndexingService;

/// <inheritdoc cref="IGoogleIndexingServiceUtil"/>
public class GoogleIndexingServiceUtil: IGoogleIndexingServiceUtil
{
    private readonly SingletonDictionary<global::Google.Apis.Indexing.v3.IndexingService> _indexingServices;

    public GoogleIndexingServiceUtil(IGoogleCredentialsUtil googleCredentialsUtil)
    {
        _indexingServices = new SingletonDictionary<global::Google.Apis.Indexing.v3.IndexingService>(async (args) =>
        {
            var fileName = (string)args!.First();

            ICredential credential = await googleCredentialsUtil.Get(fileName);

            return new global::Google.Apis.Indexing.v3.IndexingService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential
            });
        });
    }

    public ValueTask<global::Google.Apis.Indexing.v3.IndexingService> Get(string fileName)
    {
        return _indexingServices.Get(fileName, fileName);
    }

    public ValueTask Remove(string fileName)
    {
        return _indexingServices.Remove(fileName);
    }

    public void RemoveSync(string fileName)
    {
        _indexingServices.RemoveSync(fileName);
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
