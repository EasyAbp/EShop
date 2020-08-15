﻿using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace EasyAbp.EShop.Plugins.ProductTag.Samples
{
    public class SampleAppService_Tests : ProductTagApplicationTestBase
    {
        private readonly ISampleAppService _sampleAppService;

        public SampleAppService_Tests()
        {
            _sampleAppService = GetRequiredService<ISampleAppService>();
        }

        [Fact]
        public async Task GetAsync()
        {
            var result = await _sampleAppService.GetAsync();
            result.Value.ShouldBe(42);
        }

        [Fact]
        public async Task GetAuthorizedAsync()
        {
            var result = await _sampleAppService.GetAuthorizedAsync();
            result.Value.ShouldBe(42);
        }
    }
}
