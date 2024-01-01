using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Google.Credentials.Registrars;
using Soenneker.Google.IndexingService.Abstract;

namespace Soenneker.Google.IndexingService.Registrars;

/// <summary>
/// An async thread-safe singleton for the Google indexing service client
/// </summary>
public static class GoogleIndexingServiceUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IGoogleIndexingServiceUtil"/> as a singleton service. <para/>
    /// </summary>
    public static void AddGoogleIndexingServiceUtilAsSingleton(this IServiceCollection services)
    {
        services.AddGoogleCredentialsUtilAsSingleton();
        services.TryAddSingleton<IGoogleIndexingServiceUtil, GoogleIndexingServiceUtil>();
    }

    /// <summary>
    /// Adds <see cref="IGoogleIndexingServiceUtil"/> as a scoped service. <para/>
    /// </summary>
    public static void AddGoogleIndexingServiceUtilAsScoped(this IServiceCollection services)
    {
        services.AddGoogleCredentialsUtilAsScoped();
        services.TryAddScoped<IGoogleIndexingServiceUtil, GoogleIndexingServiceUtil>();
    }
}
