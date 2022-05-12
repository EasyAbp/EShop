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

[![](https://mermaid.ink/img/pako:eNqdlMtOwzAQRX_F8gaQStlHqBKPrilU7LJx7Ulr1Y_gB1JU9d-Z2G4b2lIE2ST2zD25Y4-9odwKoBX18BHBcHiWbOmYrg3Bh8VgTdQLcHn87sHdTibT-cq24xcnwPmKPDlgAQgjC2vX0iyJ7QNZMMw8Eo5nKi6l8ePHLKvI1PjogIQVwryHcBekBiJ9ArOFgjNMhPamKpLGhCcv4ozbGes0mIB-54G5gHbbPEMa69I_T2zvJCeMf1v_wdGT1a2CkPXF1lkj-2rLDOFFKS763u1UGpXo7p9iGuyFvTqpNMVnTB7rjhIRUL7m4D4lB1RyHtsur4-_X7jJ9WNU6zz70E8i8SYjv0svmOnFzPCOOPBRhT018fbRtxQ84JkKA6mPnAPmlzX8uaCjth9u2qB3QHkY0Bsm1Z_RqAR1APdlMSOwyCbia98lpUnAiAvH8zWC64juMurKEx9YiP73s9QiF92NDk02QgbhyRwIOqIanMZOwNtj0-Nqis401LTCTwENw0WvaW22mBpbgQdzKmSwjlYNw0Ua0f56mXeG0yq4CLukcgOVrO0XMuycsQ)](https://mermaid-js.github.io/mermaid-live-editor/edit#pako:eNqdlMtOwzAQRX_F8gaQStlHqBKPrilU7LJx7Ulr1Y_gB1JU9d-Z2G4b2lIE2ST2zD25Y4-9odwKoBX18BHBcHiWbOmYrg3Bh8VgTdQLcHn87sHdTibT-cq24xcnwPmKPDlgAQgjC2vX0iyJ7QNZMMw8Eo5nKi6l8ePHLKvI1PjogIQVwryHcBekBiJ9ArOFgjNMhPamKpLGhCcv4ozbGes0mIB-54G5gHbbPEMa69I_T2zvJCeMf1v_wdGT1a2CkPXF1lkj-2rLDOFFKS763u1UGpXo7p9iGuyFvTqpNMVnTB7rjhIRUL7m4D4lB1RyHtsur4-_X7jJ9WNU6zz70E8i8SYjv0svmOnFzPCOOPBRhT018fbRtxQ84JkKA6mPnAPmlzX8uaCjth9u2qB3QHkY0Bsm1Z_RqAR1APdlMSOwyCbia98lpUnAiAvH8zWC64juMurKEx9YiP73s9QiF92NDk02QgbhyRwIOqIanMZOwNtj0-Nqis401LTCTwENw0WvaW22mBpbgQdzKmSwjlYNw0Ua0f56mXeG0yq4CLukcgOVrO0XMuycsQ)
