using System;
using EasyAbp.EShop.Orders.Orders.Dtos;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderExtraFeeMapperlyTests : OrdersApplicationTestBase
    {
        private readonly IObjectMapper<EShopOrdersApplicationModule> _objectMapper;

        public OrderExtraFeeMapperlyTests()
        {
            _objectMapper = GetRequiredService<IObjectMapper<EShopOrdersApplicationModule>>();
        }

        [Fact]
        public void Should_Map_OrderExtraFee_To_OrderExtraFeeDto()
        {
            var entity = new OrderExtraFee(Guid.NewGuid(), "Shipping", "shipping-key", "Shipping Fee", 12.5m);

            var dto = _objectMapper.Map<OrderExtraFee, OrderExtraFeeDto>(entity);

            dto.ShouldNotBeNull();
            dto.Name.ShouldBe("Shipping");
            dto.Key.ShouldBe("shipping-key");
            dto.DisplayName.ShouldBe("Shipping Fee");
            dto.Fee.ShouldBe(12.5m);
        }
    }
}
