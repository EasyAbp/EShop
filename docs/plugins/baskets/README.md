# EShop.Plugins.Baskets

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FEShop%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.EShop.Plugins.Baskets.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Baskets.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.EShop.Plugins.Baskets.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Baskets.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/xyg8TrRa27?label=Discord)](https://discord.gg/xyg8TrRa27)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/EShop?style=social)](https://www.github.com/EasyAbp/EShop)

🛒 A baskets (cart) plugin for EShop. It supports both the server-side pattern and the client-side pattern.

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.EShop.Plugins.Baskets.Application
    * EasyAbp.EShop.Plugins.Baskets.Application.Contracts
    * EasyAbp.EShop.Plugins.Baskets.Domain
    * EasyAbp.EShop.Plugins.Baskets.Domain.Shared
    * EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore
    * EasyAbp.EShop.Plugins.Baskets.HttpApi
    * EasyAbp.EShop.Plugins.Baskets.HttpApi.Client
    * (Optional) EasyAbp.EShop.Plugins.Baskets.MongoDB
    * (Optional) EasyAbp.EShop.Plugins.Baskets.Web

1. Add `DependsOn(typeof(EShopXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

1. Add `builder.ConfigureEShopPluginsBaskets();` to the `OnModelCreating()` method in **MyProjectMigrationsDbContext.cs**.

1. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

## Server-side Baskets Pattern

The server-side basket is for identified(logon) users. It requests the basket [backend service APIs](https://github.com/EasyAbp/EShop/blob/dev/plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.HttpApi/EasyAbp/EShop/Plugins/Baskets/BasketItems/BasketItemController.cs) are available.

1. Before you create a basket item, use `/api/e-shop/orders/order/check-create` (POST) to check whether the current user is allowed to create an order with it.
1. Use `/api/e-shop/plugins/baskets/basket-item` (POST) to create basket items on the server side. Extra properties are allowed.
1. Use `/api/e-shop/plugins/baskets/basket-item` (GET) to get basket the item list. The returned "IsInvalid" property shows whether you can use the item to create an order.
1. Use `/api/e-shop/plugins/baskets/basket-item/{id}` (PUT) or `/api/e-shop/plugins/baskets/basket-item/{id}` (DELETE) to change an item's quantity or remove an item.

## Client-side Baskets Pattern

You should store the basket items in the client(browser) cache as a collection of [IBasketItem](https://github.com/EasyAbp/EShop/blob/dev/plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Domain.Shared/EasyAbp/EShop/Plugins/Baskets/BasketItems/IBasketItem.cs). The client-side doesn't depend on the baskets module backend service APIs.

### With Backend

If you install the backend, it provides APIs to help refresh your client-side basket items.

1. Before you create a basket item, use `/api/e-shop/orders/order/check-create` (POST) to check whether the current user is allowed to create an order with it.
1. Use `/api/e-shop/plugins/baskets/basket-item/generate-client-side-data` (POST) to refresh your client-side basket items anytime.

### Without Backend

If you don't install the backend, you can still create and store the basket items locally, but the client-side basket items will not validate and never update.

## How To Determine a Basket Pattern

The server-side baskets pattern will take effect if all the following conditions are met:
1. The baskets module backend has been installed and is available.
1. The `EasyAbp.EShop.Plugins.Baskets.EnableServerSideBaskets` setting value is "True".
1. The current user has the `EasyAbp.EShop.Plugins.Baskets.BasketItem` permission.

## What if Anonymous Users Add Basket Items and Then Log In?

The client(browser) should try to add the existing items to the server-side and remove them from the client-side. You can implement it by referring to the [index.js](https://github.com/EasyAbp/EShop/blob/dev/plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Web/Pages/EShop/Plugins/Baskets/BasketItems/BasketItem/index.js).

## Use Basket Items To Create an Order

We don't provide a built-in way to convert basket items to an order creation DTO.

Create an order manually by the front end, and remember to map the basket item's extra properties to the order creation DTO since some special product may need them.

After creating the order, the front end should clear the unused basket items.