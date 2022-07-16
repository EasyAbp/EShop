# EShop.Plugins.FlashSales

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FEShop%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![Discord online](https://badgen.net/discord/online-members/S6QaezrCRq?label=Discord)](https://discord.gg/S6QaezrCRq)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/EShop?style=social)](https://www.github.com/EasyAbp/EShop)

A flash-sales plugin for EShop.

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.EShop.Orders.Plugins.FlashSales.Application (install at EasyAbp.EShop.Orders.Application location)
    * EasyAbp.EShop.Products.Plugins.FlashSales.Application (install at EasyAbp.EShop.Products.Application location)
    * EasyAbp.EShop.Plugins.FlashSales.Application
    * EasyAbp.EShop.Plugins.FlashSales.Application.Contracts
    * EasyAbp.EShop.Plugins.FlashSales.Domain
    * EasyAbp.EShop.Plugins.FlashSales.Domain.Shared
    * EasyAbp.EShop.Plugins.FlashSales.EntityFrameworkCore
    * EasyAbp.EShop.Plugins.FlashSales.HttpApi
    * EasyAbp.EShop.Plugins.FlashSales.HttpApi.Client
    * (Optional) EasyAbp.EShop.Plugins.FlashSales.MongoDB
    * (Optional) EasyAbp.EShop.Plugins.FlashSales.Web
    * (Special Optional) EasyAbp.EShop.Products.Plugins.FlashSales.Application.Contracts (install at EasyAbp.EShop.Products.Application.Contracts location)
    * (SpecialOptional) EasyAbp.EShop.Products.Plugins.FlashSales.HttpApi (install at EasyAbp.EShop.Products.HttpApi location)
    * (Special Optional) EasyAbp.EShop.Products.Plugins.FlashSales.HttpApi.Client (install at EasyAbp.EShop.Products.HttpApi.Client location)
   > When you do not open the dynamic API for the `EasyAbp.EShop.Products` module, these `Special Optional` modules must be installed.

2. Add `DependsOn(typeof(EShopXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

3. Add `builder.ConfigureEShopPluginsFlashSales();` to the `OnModelCreating()` method in **MyProjectDbContext.cs**.

4. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

## Usage

### Admins

1. Create a published flash-sale plan.

### Customers

1. Using `/api/e-shop/plugins/flash-sales/flash-sale-plan/{planId}/pre-order` (POST) to pre-order. It will return the expires time if the flash-sale plan is available.
2. Within the expires time, using `/api/e-shop/plugins/flash-sales/flash-sale-plan/{planId}/order` (POST) to request flash purchase order.
3. Using `/api/e-shop/plugins/flash-sales/flash-sale-result/{resultId}` (GET) to query the result.

    > Continuously pulled until the result is no longer `Pending`. If the status is `Successful`, the order will be included. If the status `Failed`, the reason will be included.
