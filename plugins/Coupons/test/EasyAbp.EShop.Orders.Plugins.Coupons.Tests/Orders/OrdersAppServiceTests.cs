using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Plugins.Coupons.OrderDiscount;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Products.Products;
using Shouldly;
using Volo.Abp.Data;
using Xunit;
using static EasyAbp.EShop.Orders.Authorization.OrdersPermissions;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class OrdersAppServiceTests : EShopOrdersPluginsCouponsTestBase
    {
        private readonly ICouponRepository _couponRepository;
        private readonly ICouponTemplateRepository _couponTemplateRepository;

        public OrdersAppServiceTests()
        {
            _couponRepository = GetRequiredService<ICouponRepository>();
            _couponTemplateRepository = GetRequiredService<ICouponTemplateRepository>();
        }

        [Theory]
        //Meet the condition discount 1 time
        [InlineData(5, 2.3, 11, 2.3, CouponType.Normal, false)]
        [InlineData(5, 2.3, 10, 2.3, CouponType.Normal, false)]
        [InlineData(5, 2.3, 9, 2.3, CouponType.Normal, false)]
        //The discount condition is not met
        [InlineData(5, 2.3, 4, 0, CouponType.Normal, true)]

        //Meet the condition discount 2 time
        [InlineData(5, 2.3, 11, 4.6, CouponType.PerMeet, false)]
        [InlineData(5, 2.3, 10, 4.6, CouponType.PerMeet, false)]

        //Meet the condition discount 1 time
        [InlineData(5, 2.3, 9, 2.3, CouponType.PerMeet, false)]
        //The discount condition is not met
        [InlineData(5, 2.3, 4, 0, CouponType.PerMeet, true)]
        public async Task Should_CouponType_DiscountAmount(decimal couponConditionAmount, decimal couponDiscountAmount,
            decimal productSkuPrice, decimal discountAmount, CouponType couponType, bool throws)
        {
            var template = new CouponTemplate(Guid.NewGuid(), null, null, couponType, "UName", "DName", "Desc", null
                , DateTime.Now.AddDays(-1), DateTime.Now.AddDays(1), couponConditionAmount, couponDiscountAmount, "USD",
                true, null);
            var coupon = new Coupon(Guid.NewGuid(), null, template.Id,
                Guid.Parse("2e701e62-0953-4dd3-910b-dc6cc93ccb0d"), null, null);
            await WithUnitOfWorkAsync(async () =>
            {
                // Arrange
                await _couponTemplateRepository.InsertAsync(template);
                await _couponRepository.InsertAsync(coupon);
            });
            Order order = null;
            var prodoctSku = new ProductSkuEto
            {
                Id = OrderTestData.ProductSku2Id,
                AttributeOptionIds = new List<Guid>(),
                Price = productSkuPrice,
                Currency = "USD",
                OrderMinQuantity = 1,
                OrderMaxQuantity = 100,
            };
            await WithUnitOfWorkAsync(async () =>
            {
                var orderGenerator = GetRequiredService<INewOrderGenerator>();

                var createOrderLine =
                    new CreateOrderLineInfoModel(OrderTestData.Product1Id, OrderTestData.ProductSku2Id, 1);
                var createOrderInfoModel = new CreateOrderInfoModel(OrderTestData.Store1Id, null,
                    new List<CreateOrderLineInfoModel>
                    {
                        createOrderLine
                    }
                );
                createOrderInfoModel.SetProperty("CouponId", coupon.Id);

                var dic = new Dictionary<Guid, IProduct>
                {
                    {
                        OrderTestData.Product1Id,
                        new ProductEto
                        {
                            Id = OrderTestData.Product1Id,
                            ProductSkus = new List<ProductSkuEto> { prodoctSku }
                        }
                    }
                };

                if (throws)
                {
                    await Should.ThrowAsync<OrderDoesNotMeetCouponUsageConditionException>(() =>
                        orderGenerator.GenerateAsync(Guid.NewGuid(), createOrderInfoModel, dic,
                            new Dictionary<Guid, DateTime>()));
                    return;
                }
                else
                {
                    order = await orderGenerator.GenerateAsync(Guid.NewGuid(), createOrderInfoModel, dic,
                        new Dictionary<Guid, DateTime>());
                }
            });

            //assert

            var orderLine = order.OrderLines[0];

            var orderLine2ExpectedPrice = 1 * prodoctSku.Price - discountAmount;
            order.ActualTotalPrice.ShouldBe(orderLine2ExpectedPrice);
            order.TotalDiscount.ShouldBe(order.TotalPrice - order.ActualTotalPrice);
            order.OrderDiscounts.Count.ShouldBe(1);
            var orderDiscount = order.OrderDiscounts.SingleOrDefault(x =>
                x.OrderId == order.Id && x.OrderLineId == orderLine.Id && x.EffectGroup == "Coupon");
            orderDiscount.ShouldNotBeNull();
            orderDiscount.DiscountedAmount.ShouldBe(discountAmount);
        }
    }
}