# EShop.Plugins.Promotions

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FEShop%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.EShop.Plugins.Promotions.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Promotions.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.EShop.Plugins.Promotions.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Promotions.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/xyg8TrRa27?label=Discord)](https://discord.gg/xyg8TrRa27)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/EShop?style=social)](https://www.github.com/EasyAbp/EShop)

A promotion discount provider plugin for EShop.

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

   - EasyAbp.EShop.Orders.Plugins.Promotions.Domain (install at EasyAbp.EShop.Orders.Domain location)
   - EasyAbp.EShop.Products.Plugins.Promotions.Domain (install at EasyAbp.EShop.Products.Domain location)
   - EasyAbp.EShop.Plugins.Promotions.Application
   - EasyAbp.EShop.Plugins.Promotions.Application.Contracts
   - EasyAbp.EShop.Plugins.Promotions.Domain
   - EasyAbp.EShop.Plugins.Promotions.Domain.Shared
   - EasyAbp.EShop.Plugins.Promotions.EntityFrameworkCore
   - EasyAbp.EShop.Plugins.Promotions.HttpApi
   - EasyAbp.EShop.Plugins.Promotions.HttpApi.Client
   - (Optional) EasyAbp.EShop.Plugins.Promotions.MongoDB
   - (Optional) EasyAbp.EShop.Plugins.Promotions.Web

2. Add `DependsOn(typeof(EShopXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

3. Add `builder.ConfigureEShopPluginsPromotions();` to the `OnModelCreating()` method in **MyProjectDbContext.cs**.

4. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

## Concepts

This promotion plugin module provides an easy way to define EShop's product discounts and order discounts.

### Promotion Type

Promotion type is defined to provide programs for different discount logics.

We have created [SimpleProductDiscount](https://github.com/EasyAbp/EShop/tree/dev/plugins/Promotions/src/EasyAbp.EShop.Plugins.Promotions.Domain/EasyAbp/EShop/Plugins/Promotions/PromotionTypes/SimpleProductDiscount) and [MinQuantityOrderDiscount](https://github.com/EasyAbp/EShop/tree/dev/plugins/Promotions/src/EasyAbp.EShop.Plugins.Promotions.Domain/EasyAbp/EShop/Plugins/Promotions/PromotionTypes/MinQuantityOrderDiscount) promotion type definitions for you.
You can define custom promotion types yourself if you need different discount logic.

### Product Discount

Product discounts subtract the price of products before customers place an order.

For a product SKU, if there are more than one promotion offering product discounts, customers will see all of them,
but only the one with the highest discount amount will be applied,
since they have the same `EffectGroup = "Promotion"`.

The promotion product discounts coexist with other types of discounts with different EffectGroup.

### Order Discount

Order discounts subtract the price of products after customers place an order.

For an order line, if there are more than one promotion offering order discounts,
only the one with the highest discount amount will be applied,
since they have the same `EffectGroup = "Promotion"`.

The promotion order discounts coexist with other types of discounts with different EffectGroup.

### The Best Combination of Discounts

If your product or order has many related discounts with conflicts, EShop will find the best combination of discounts using BFS.

See these codes for more:

* [ProductDiscountResolver](https://github.com/EasyAbp/EShop/blob/dev/modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Domain/EasyAbp/EShop/Products/Products/ProductDiscountResolver.cs)
* [OrderDiscountResolver](https://github.com/EasyAbp/EShop/blob/dev/modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Domain/EasyAbp/EShop/Orders/Orders/OrderDiscountResolver.cs)
* [DemoOrderDiscountProvider](https://github.com/EasyAbp/EShop/blob/dev/modules/EasyAbp.EShop.Orders/test/EasyAbp.EShop.Orders.Domain.Tests/Orders/DemoOrderDiscountProvider.cs) and [OrderDiscountProviderTests](https://github.com/EasyAbp/EShop/blob/dev/modules/EasyAbp.EShop.Orders/test/EasyAbp.EShop.Orders.Domain.Tests/Orders/OrderDiscountProviderTests.cs)

## Usage

### Create a Promotion for Product Discounts

You can create a promotion with the preset promotion type [SimpleProductDiscount](https://github.com/EasyAbp/EShop/tree/dev/plugins/Promotions/src/EasyAbp.EShop.Plugins.Promotions.Domain/EasyAbp/EShop/Plugins/Promotions/PromotionTypes/SimpleProductDiscount).
It offers direct product discounts for the products you specify.

![SimpleProductDiscount](/docs/plugins/promotions/images/SimpleProductDiscount.apng)

### Create a Promotion for Order Discounts

You can create a promotion with the preset promotion type [SimpleProductDiscount](https://github.com/EasyAbp/EShop/tree/dev/plugins/Promotions/src/EasyAbp.EShop.Plugins.Promotions.Domain/EasyAbp/EShop/Plugins/Promotions/PromotionTypes/SimpleProductDiscount).
It discounts an order line if the `OrderLine.Quantity >= Configurations.MinQuantity`.

![MinQuantityOrderDiscount](/docs/plugins/promotions/images/MinQuantityOrderDiscount.apng)
