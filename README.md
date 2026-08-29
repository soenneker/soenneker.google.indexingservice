[![](https://img.shields.io/nuget/v/soenneker.google.indexingservice.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.google.indexingservice/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.google.indexingservice/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.google.indexingservice/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.google.indexingservice.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.google.indexingservice/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.google.indexingservice/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.google.indexingservice/actions/workflows/codeql.yml)

# Soenneker.Google.IndexingService

An async thread-safe singleton for the Google indexing service client.

## Install

```bash
dotnet add package Soenneker.Google.IndexingService
```

## Quick start

```csharp
using Soenneker.Google.IndexingService.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddGoogleIndexingServiceUtilAsSingleton();
```

Adds `IGoogleIndexingServiceUtil` as a singleton service.

## What you get

- `IGoogleIndexingServiceUtil` — An async thread-safe singleton for the Google indexing service client.
- `GoogleIndexingServiceUtilRegistrar` — An async thread-safe singleton for the Google indexing service client.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IGoogleIndexingServiceUtil.Remove(fileName, cancellationToken)` | Removes and disposes the cached indexing client associated with a credential file. | A task whose result is `true` when a cached client was removed; otherwise, `false`. |
| `GoogleIndexingServiceUtilRegistrar.AddGoogleIndexingServiceUtilAsSingleton(services)` | Adds `IGoogleIndexingServiceUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `GoogleIndexingServiceUtilRegistrar.AddGoogleIndexingServiceUtilAsScoped(services)` | Adds `IGoogleIndexingServiceUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
- Calls that return a cached or singleton value reuse the same instance until the owning service is disposed.
- Dispose instances you own when their scope ends so held resources can be released.
