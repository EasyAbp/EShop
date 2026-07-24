using System;
using EasyAbp.EShop.Payments.Payments.Dtos;
using Shouldly;
using Volo.Abp.ObjectMapping;
using Xunit;

namespace EasyAbp.EShop.Payments.Payments
{
    public class PaymentItemMapperlyTests : PaymentsApplicationTestBase
    {
        private readonly IObjectMapper<EShopPaymentsApplicationModule> _objectMapper;

        public PaymentItemMapperlyTests()
        {
            _objectMapper = GetRequiredService<IObjectMapper<EShopPaymentsApplicationModule>>();
        }

        [Fact]
        public void Should_Map_PaymentItem_To_PaymentItemDto()
        {
            var entity = new PaymentItem(Guid.NewGuid(), "TestItemType", "item-key", 100m, 10m, 90m, 0m, 0m);

            var dto = _objectMapper.Map<PaymentItem, PaymentItemDto>(entity);

            dto.ShouldNotBeNull();
            dto.Id.ShouldBe(entity.Id);
            dto.ItemType.ShouldBe("TestItemType");
            dto.ItemKey.ShouldBe("item-key");
            dto.OriginalPaymentAmount.ShouldBe(100m);
            dto.ActualPaymentAmount.ShouldBe(90m);
        }
    }
}
