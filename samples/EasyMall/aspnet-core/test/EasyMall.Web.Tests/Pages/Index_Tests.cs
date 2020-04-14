using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EasyMall.Pages
{
    public class Index_Tests : EasyMallWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
