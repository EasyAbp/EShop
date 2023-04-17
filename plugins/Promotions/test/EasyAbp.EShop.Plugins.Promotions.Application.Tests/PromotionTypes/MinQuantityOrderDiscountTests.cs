using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Plugins.Promotions.Promotions;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes.MinQuantityOrderDiscount;
using EasyAbp.EShop.Products.Products;
using Shouldly;
using Volo.Abp.Json;
using Xunit;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes;

public class MinQuantityOrderDiscountTests : PromotionsApplicationTestBase
{
    private IJsonSerializer JsonSerializer { get; }
    private PromotionManager PromotionManager { get; }
    private IPromotionRepository PromotionRepository { get; }
    private IOrderDiscountResolver OrderDiscountResolver { get; }
    private MinQuantityOrderDiscountPromotionHandler Handler { get; }

    public MinQuantityOrderDiscountTests()
    {
        JsonSerializer = GetRequiredService<IJsonSerializer>();
        PromotionManager = GetRequiredService<PromotionManager>();
        PromotionRepository = GetRequiredService<IPromotionRepository>();
        OrderDiscountResolver = GetRequiredService<IOrderDiscountResolver>();
        Handler = GetRequiredService<MinQuantityOrderDiscountPromotionHandler>();
    }

    [Fact]
    public async Task Should_Add_Order_Candidate_Discount_Preview()
    {
        var promotion = await CreatePromotionAsync();

        var productSku = new ProductSkuEto
        {
            Currency = "USD"
        };

        var product = new ProductEto
        {
            ProductGroupName = "MyProductGroup",
            ProductSkus = new List<ProductSkuEto> { productSku }
        };

        var model = new ProductRealTimePriceInfoModel(product.Id, productSku.Id, 1.00m);

        await Handler.HandleProductAsync(model, promotion, product, productSku);

        model.OrderDiscountPreviews.Count.ShouldBe(1);

        var orderDiscount = model.OrderDiscountPreviews.First();
        orderDiscount.ShouldNotBeNull();
        orderDiscount.EffectGroup.ShouldBe(PromotionConsts.PromotionEffectGroup);
        orderDiscount.DisplayName.ShouldBe("test");
        orderDiscount.FromTime.ShouldBeNull();
        orderDiscount.ToTime.ShouldBeNull();
    }

    [Fact]
    public async Task Should_Discount_OrderLine_If_Quantity_Is_2()
    {
        var promotion = await CreatePromotionAsync();

        var order = new Order(Guid.NewGuid(), null, PromotionsTestConsts.StoreId, Guid.NewGuid(), "USD", 2.00m, 0m,
            2.00m, 2.00m, null, null);

        order.OrderLines.Add(new OrderLine(order.Id, Guid.NewGuid(), Guid.NewGuid(), null, DateTime.Now, DateTime.Now,
            "MyProductGroup", "MyProductGroup", null, "Test", InventoryStrategy.NoNeed, null, null, null, "USD", 1.00m,
            2.00m, 0.00m, 2.00m, 2));

        var context = new OrderDiscountContext(DateTime.Now, order, new Dictionary<Guid, IProduct>());

        await Handler.HandleOrderAsync(context, promotion);

        context.CandidateDiscounts.Count.ShouldBe(1);
        context.CandidateDiscounts[0].EffectGroup.ShouldBe(PromotionConsts.PromotionEffectGroup);
        context.CandidateDiscounts[0].Name.ShouldBe(PromotionConsts.PromotionDiscountName);
        context.CandidateDiscounts[0].Key.ShouldBe(promotion.UniqueName);
        context.CandidateDiscounts[0].DisplayName.ShouldBe(promotion.DisplayName);
        context.CandidateDiscounts[0].AffectedOrderLineIds.Count.ShouldBe(1);
        context.CandidateDiscounts[0].AffectedOrderLineIds.ShouldContain(order.OrderLines[0].Id);

        var distributionModels = await OrderDiscountResolver.ResolveAsync(order, new Dictionary<Guid, IProduct>());

        distributionModels.Count.ShouldBe(1);
        distributionModels[0].DiscountInfoModel.Name.ShouldBe(PromotionConsts.PromotionDiscountName);
        distributionModels[0].DiscountInfoModel.Key.ShouldBe(promotion.UniqueName);
        distributionModels[0].Distributions.Count.ShouldBe(1);
        distributionModels[0].Distributions.First().Value.ShouldBe(0.02m);
    }

    [Fact]
    public async Task Should_Not_Discount_OrderLine_If_Quantity_Is_1()
    {
        var promotion = await CreatePromotionAsync();

        var order = new OrderEto
        {
            OrderLines = new List<OrderLineEto>
            {
                new()
                {
                    ProductGroupName = "MyProductGroup",
                    Currency = "USD",
                    UnitPrice = 1.00m,
                    TotalPrice = 1.00m,
                    TotalDiscount = 0m,
                    ActualTotalPrice = 1.00m,
                    Quantity = 1,
                }
            }
        };

        var context = new OrderDiscountContext(DateTime.Now, order, new Dictionary<Guid, IProduct>());

        await Handler.HandleOrderAsync(context, promotion);

        context.CandidateDiscounts.ShouldBeEmpty();
    }

    private async Task<Promotion> CreatePromotionAsync()
    {
        return await PromotionRepository.InsertAsync(await PromotionManager.CreateAsync(PromotionsTestConsts.StoreId,
            MinQuantityOrderDiscountPromotionHandler.MinQuantityOrderDiscountPromotionTypeName, "test", "test",
            JsonSerializer.Serialize(new MinQuantityOrderDiscountConfigurations
            {
                Discounts = new List<MinQuantityOrderDiscountModel>
                {
                    new(new List<ProductScopeModel>
                    {
                        new("MyProductGroup", null, null, null)
                    }, 2, new DynamicDiscountAmountModel("USD", 0.01m, 0m, null))
                }
            }), null, null, false, 10000), true);
    }
}