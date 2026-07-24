using System;
using EasyAbp.EShop.Stores.Stores.Dtos;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreMapperlyTests : StoresApplicationTestBase
    {
        private readonly IObjectMapper<EShopStoresApplicationModule> _objectMapper;

        public StoreMapperlyTests()
        {
            _objectMapper = GetRequiredService<IObjectMapper<EShopStoresApplicationModule>>();
        }

        [Fact]
        public void Should_Map_Store_To_StoreDto()
        {
            var entity = new Store(Guid.NewGuid(), null, "Test Store");

            var dto = _objectMapper.Map<Store, StoreDto>(entity);

            dto.ShouldNotBeNull();
            dto.Id.ShouldBe(entity.Id);
            dto.Name.ShouldBe("Test Store");
        }
    }
}
