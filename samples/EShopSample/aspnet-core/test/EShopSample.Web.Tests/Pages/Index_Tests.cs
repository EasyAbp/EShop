using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EShopSample.Pages
{
    public class Index_Tests : EShopSampleWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
