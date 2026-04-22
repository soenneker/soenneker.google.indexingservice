using Soenneker.Google.IndexingService.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Google.IndexingService.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class GoogleIndexingServiceUtilTests : HostedUnitTest
{
    private readonly IGoogleIndexingServiceUtil _util;

    public GoogleIndexingServiceUtilTests(Host host) : base(host)
    {
        _util = Resolve<IGoogleIndexingServiceUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
