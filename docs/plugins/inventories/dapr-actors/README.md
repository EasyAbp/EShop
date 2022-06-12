# EShop.Plugins.Inventories.DaprActors

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FEShop%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.EShop.Plugins.Inventories.DaprActors.Abstractions.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Inventories.DaprActors.Abstractions)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.EShop.Plugins.Inventories.DaprActors.Abstractions.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Inventories.DaprActors.Abstractions)
[![Discord online](https://badgen.net/discord/online-members/S6QaezrCRq?label=Discord)](https://discord.gg/S6QaezrCRq)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/EShop?style=social)](https://www.github.com/EasyAbp/EShop)

EShop product-inventory implementation of [Dapr Actors](https://docs.dapr.io/developing-applications/building-blocks/actors/actors-overview).

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

   * EasyAbp.EShop.Products.DaprActorsInventory.Domain _(install at EasyAbp.EShop.Products.Domain location)_
   * EasyAbp.EShop.Plugins.Inventories.DaprActors.AspNetCore _(install at a host project to run Actors)_

2. Add `DependsOn(typeof(EShopXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

3. Configure a state store for the inventory actor. ([see how](https://docs.dapr.io/reference/api/state_api/#configuring-state-store-for-actors))

## Usage

1. Configure the DaprActors inventory provider as default.
   ```csharp
   Configure<EShopProductsOptions>(options =>
   {
       // Configure as the default inventory provider
       options.DefaultInventoryProviderName = "DaprActors";

       // Configure as the default inventory provider for MyProductGroup
       options.Groups.Configure<MyProductGroup>(group =>
       {
           group.DefaultInventoryProviderName = "DaprActors";
       });
   });
   ```
   > Better to use `DaprActorsProductInventoryProvider.DaprActorsProductInventoryProviderName` instead of `"DaprActors"` as the provider name.

2. Create a product and set `InventoryProviderName` to `DaprActors`. Then the product is specified to use the Dapr Actors inventory provider. 
