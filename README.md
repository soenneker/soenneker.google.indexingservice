[![](https://img.shields.io/nuget/v/soenneker.google.indexingservice.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.google.indexingservice/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.google.indexingservice/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.google.indexingservice/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.google.indexingservice.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.google.indexingservice/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.google.indexingservice/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.google.indexingservice/actions/workflows/codeql.yml)

# Soenneker.Google.IndexingService

A lazy, thread-safe provider for Google Indexing API clients keyed by service-account credential file.

## Install

```bash
dotnet add package Soenneker.Google.IndexingService
```

## Credential file

This package uses `Soenneker.Google.Credentials`. Place a service-account JSON file beneath `LocalResources` in the application output and pass its resource-relative filename to `Get()`.

```xml
<Content Include="LocalResources\google-indexing.json"
         CopyToOutputDirectory="PreserveNewest" />
```

## Register

```csharp
using Soenneker.Google.IndexingService.Registrars;
using Microsoft.Extensions.DependencyInjection;

services.AddGoogleIndexingServiceUtilAsSingleton();
```

Singleton registration is recommended for this client provider. Scoped application utilities can use it and be disposed without destroying the cached Google client. `AddGoogleIndexingServiceUtilAsScoped()` is available only when each scope deliberately needs separate credential and client caches.

## Publish a notification

```csharp
using Google.Apis.Indexing.v3.Data;

Google.Apis.Indexing.v3.IndexingService service =
    await indexingServices.Get("google-indexing.json", cancellationToken);

var notification = new UrlNotification
{
    Url = "https://example.com/jobs/software-engineer",
    Type = "URL_UPDATED"
};

await service.UrlNotifications
    .Publish(notification)
    .ExecuteAsync(cancellationToken);
```

The service account must be authorized for the target site. This package creates the authenticated client; it does not decide whether a URL or notification type is eligible for Google's Indexing API.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `Get(fileName)` | Gets or creates the authenticated client for a credential file. | Reuses the same client for that filename within the provider lifetime. |
| `Remove(fileName)` | Asynchronously removes and disposes a cached client. | Returns whether an entry existed. |
| `RemoveSync(fileName)` | Synchronously removes and disposes a cached client. | Use only when asynchronous removal is unavailable. |

## Practical notes

- Removing a client does not remove the underlying cached credential from `IGoogleCredentialsUtil`.
- Do not use a client concurrently with removing that same filename; removal disposes the cached service.
- Let the DI container dispose registered providers. Dispose manually constructed providers yourself.
