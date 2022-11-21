# EShop.Plugins.Inventories.OrleansGrains

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FEShop%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.EShop.Plugins.Inventories.OrleansGrains.Abstractions.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Inventories.OrleansGrains.Abstractions)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.EShop.Plugins.Inventories.OrleansGrains.Abstractions.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Inventories.OrleansGrains.Abstractions)
[![Discord online](https://badgen.net/discord/online-members/xyg8TrRa27?label=Discord)](https://discord.gg/xyg8TrRa27)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/EShop?style=social)](https://www.github.com/EasyAbp/EShop)

EShop product-inventory implementation of [Orleans Grains](https://docs.microsoft.com/en-us/dotnet/orleans/grains).

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

   * EasyAbp.EShop.Products.OrleansGrainsInventory.Domain _(install at EasyAbp.EShop.Products.Domain location)_
   * EasyAbp.EShop.Plugins.Inventories.OrleansGrains.Silo _(install at a host project to run Grains)_

2. Add `DependsOn(typeof(EShopXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

3. Open `Program.cs` in the host project to create an Orleans Silo. (see Microsoft's [document](https://docs.microsoft.com/en-us/dotnet/orleans/host/configuration-guide/server-configuration) for more information)

   ```csharp
   builder.Host.AddAppSettingsSecretsJson()
       .UseAutofac()
       .UseSerilog()
       .UseOrleans(c =>
       {
           c.UseLocalhostClustering()   // for test only
           c.AddMemoryGrainStorage(InventoryGrain.StorageProviderName);   // for test only
       });
   ```

## Usage

1. Configure the OrleansGrains inventory provider as default.
   ```csharp
   Configure<EShopProductsOptions>(options =>
   {
       // Configure as the default inventory provider
       options.DefaultInventoryProviderName = "OrleansGrains";

       // Configure as the default inventory provider for MyProductGroup
       options.Groups.Configure<MyProductGroup>(group =>
       {
           group.DefaultInventoryProviderName = "OrleansGrains";
       });
   });
   ```
   > Better to use `OrleansGrainsProductInventoryProvider.OrleansGrainsProductInventoryProviderName` instead of `"OrleansGrains"` as the provider name.

2. Create a product and set `InventoryProviderName` to `OrleansGrains`. Then the product is specified to use the Orleans Grains inventory provider. 