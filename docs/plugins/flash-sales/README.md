# EShop.Plugins.FlashSales

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FEShop%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.EShop.Plugins.FlashSales.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.FlashSales.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.EShop.Plugins.FlashSales.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.FlashSales.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/S6QaezrCRq?label=Discord)](https://discord.gg/S6QaezrCRq)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/EShop?style=social)](https://www.github.com/EasyAbp/EShop)

A flash-sales plugin for EShop.

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

   - EasyAbp.EShop.Orders.Plugins.FlashSales.Application (install at EasyAbp.EShop.Orders.Application location)
   - EasyAbp.EShop.Products.Plugins.FlashSales.Application (install at EasyAbp.EShop.Products.Application location)
   - EasyAbp.EShop.Plugins.FlashSales.Application
   - EasyAbp.EShop.Plugins.FlashSales.Application.Contracts
   - EasyAbp.EShop.Plugins.FlashSales.Domain
   - EasyAbp.EShop.Plugins.FlashSales.Domain.Shared
   - EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore
   - EasyAbp.EShop.Plugins.FlashSales.HttpApi
   - EasyAbp.EShop.Plugins.FlashSales.HttpApi.Client
   - (Optional) EasyAbp.EShop.Plugins.FlashSales.MongoDB
   - (Optional) EasyAbp.EShop.Plugins.FlashSales.Web
   - (Optional) EasyAbp.EShop.Products.Plugins.FlashSales.Application.Contracts (install at EasyAbp.EShop.Products.Application.Contracts location)
   - (Optional) EasyAbp.EShop.Products.Plugins.FlashSales.HttpApi (install at EasyAbp.EShop.Products.HttpApi location)
   - (Optional) EasyAbp.EShop.Products.Plugins.FlashSales.HttpApi.Client (install at EasyAbp.EShop.Products.HttpApi.Client location)

2. Add `DependsOn(typeof(EShopXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

3. Add `builder.ConfigureEShopPluginsFlashSales();` to the `OnModelCreating()` method in **MyProjectDbContext.cs**.

4. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

## Usage

### Admins

1. Create a published flash-sale plan.

### Customers

1. Use `/api/e-shop/plugins/flash-sales/flash-sale-plan/{planId}/pre-order` (POST) to pre-order. It will return an expiration time if your pre-order request succeeds. Don't forget to re-invoke this API to refresh your request before the expiration time.
2. When the flash sale starts, use `/api/e-shop/plugins/flash-sales/flash-sale-plan/{planId}/order` (POST) to create your order. If you are fast enough, it will occupy the inventory and create an order for you in the background.
3. If you are told that you have succeeded, continuous use `/api/e-shop/plugins/flash-sales/flash-sale-result/current/{planId}` (GET) to query the order creation result until it succeeds or fails.

## Stress Test

See the [stress test report](https://github.com/gdlcf88/eshop-flashsales-k6-stress-test/tree/main/EShop3.0Preview18-standalone).