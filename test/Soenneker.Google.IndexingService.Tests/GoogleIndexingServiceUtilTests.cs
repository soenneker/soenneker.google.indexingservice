using Soenneker.Google.IndexingService.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;


namespace Soenneker.Google.IndexingService.Tests;

[Collection("Collection")]
public class GoogleIndexingServiceUtilTests : FixturedUnitTest
{
    private readonly IGoogleIndexingServiceUtil _util;

    public GoogleIndexingServiceUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _util = Resolve<IGoogleIndexingServiceUtil>(true);
    }
}
