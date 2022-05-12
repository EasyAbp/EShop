# EShop.Plugins.Booking

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FEShop%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.EShop.Plugins.Booking.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Booking.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.EShop.Plugins.Booking.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Booking.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/S6QaezrCRq?label=Discord)](https://discord.gg/S6QaezrCRq)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/EShop?style=social)](https://www.github.com/EasyAbp/EShop)

A booking-business plugin for EShop. It extends EShop to use the [EasyAbp.BookingService](https://github.com/EasyAbp/BookingService) module to help end-users to book some assets online.

## Installation

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.EShop.Orders.Plugins.Booking (install at EasyAbp.EShop.Orders.Application location)
    * (Optional) EasyAbp.EShop.Payments.Plugins.Booking (install at EasyAbp.EShop.Payments.Application location)
    * EasyAbp.EShop.Plugins.Booking.Application
    * EasyAbp.EShop.Plugins.Booking.Application
    * EasyAbp.EShop.Plugins.Booking.Application.Contracts
    * EasyAbp.EShop.Plugins.Booking.Domain
    * EasyAbp.EShop.Plugins.Booking.Domain.Shared
    * EasyAbp.EShop.Plugins.Booking.EntityFrameworkCore
    * EasyAbp.EShop.Plugins.Booking.HttpApi
    * EasyAbp.EShop.Plugins.Booking.HttpApi.Client
    * (Optional) EasyAbp.EShop.Plugins.Booking.MongoDB
    * (Optional) EasyAbp.EShop.Plugins.Booking.Web

   > Skip installing the `EasyAbp.EShop.Payments.Plugins.Booking` module if you don't want to check assets are bookable during payment.

1. Add `DependsOn(typeof(EShopXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

1. Add `builder.ConfigureEShopPluginsBooking();` to the `OnModelCreating()` method in **MyProjectMigrationsDbContext.cs**.

1. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

## Usage

[![](https://mermaid.ink/img/pako:eNqtlMtOwzAQRX_F8gaQQtlHqBItWVOo2GXj2pPWqmMHP5Ciqv_OJHbb0BcgkU0Sz9zjO_bYG8qNAJpTBx8BNIdnyZaW1aUm-Lw7sPfjcTFfmWb0YgVYl5OpBeaBMLIwZi31kpguEAXDzCPhaKbCUmo3mkRZTgrtggXiVwhzDvyDlzUQ6XowWyg4w0RoZyon_T_hvRdxxu2MtTVoj37nnlmPdps4Qipj-zlPbO8k_2X8gp-pqRsFPuqTqbM29rWmEcKTUvzCdR7DKbqbUxTeXNmpk0r7-IzJY91RIgLS1xzsp-SASs5D08b1cY8LO76dBLWOo0_dIBLvIvK79IqZTsw0b4kFF5TfU3vePvrWBw94pvxA6gLngPlpDS8XdNT0w00bdA4oBwN6xaT6MxqVoA7griymBRZZBXztuyQ1CWhx5XC-BrAtqduIunHEeeaD-_kkNchFd9mhyTJkEN6bA0EzWoOtsRPwrth0uJKisxpKmuOngIrhope01FtMDY3AY1kI6Y2lecVwkTLKgjfzVnOaextgl5Tum5S1_QLWRJZD)](https://mermaid-js.github.io/mermaid-live-editor/edit#pako:eNqtlMtOwzAQRX_F8gaQQtlHqBItWVOo2GXj2pPWqmMHP5Ciqv_OJHbb0BcgkU0Sz9zjO_bYG8qNAJpTBx8BNIdnyZaW1aUm-Lw7sPfjcTFfmWb0YgVYl5OpBeaBMLIwZi31kpguEAXDzCPhaKbCUmo3mkRZTgrtggXiVwhzDvyDlzUQ6XowWyg4w0RoZyon_T_hvRdxxu2MtTVoj37nnlmPdps4Qipj-zlPbO8k_2X8gp-pqRsFPuqTqbM29rWmEcKTUvzCdR7DKbqbUxTeXNmpk0r7-IzJY91RIgLS1xzsp-SASs5D08b1cY8LO76dBLWOo0_dIBLvIvK79IqZTsw0b4kFF5TfU3vePvrWBw94pvxA6gLngPlpDS8XdNT0w00bdA4oBwN6xaT6MxqVoA7griymBRZZBXztuyQ1CWhx5XC-BrAtqduIunHEeeaD-_kkNchFd9mhyTJkEN6bA0EzWoOtsRPwrth0uJKisxpKmuOngIrhope01FtMDY3AY1kI6Y2lecVwkTLKgjfzVnOaextgl5Tum5S1_QLWRJZD)
