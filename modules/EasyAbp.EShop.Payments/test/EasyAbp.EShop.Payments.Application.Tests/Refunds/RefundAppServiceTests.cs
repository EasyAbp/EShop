using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using Xunit;

namespace EasyAbp.EShop.Payments.Refunds
{
    public class RefundAppServiceTests : PaymentsApplicationTestBase
    {
        private readonly IRefundAppService _refundAppService;

        public RefundAppServiceTests()
        {
            _refundAppService = GetRequiredService<IRefundAppService>();
        }

        // TODO: There may be BUG.
        [Fact]
        public async Task Should_Refund_Success()
        {
            // Arrange
            var request = new CreateEShopRefundInput
            {
                DisplayReason = "Reason",
                CustomerRemark = "Customer Remark",
                PaymentId = PaymentsTestData.Payment1,
                StaffRemark = "StaffRemark",
                RefundItems = new List<CreateEShopRefundItemInput>
                {
                    new CreateEShopRefundItemInput
                    {
                        CustomerRemark = "CustomerRemark",
                        OrderId = PaymentsTestData.Order1,
                        StaffRemark = "StaffRemark"
                    }
                }
            };

            // Act & Assert
            await _refundAppService.CreateAsync(request);
        }
    }
}
