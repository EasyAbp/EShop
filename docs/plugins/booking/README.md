# EShop.Plugins.Booking

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FEShop%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.EShop.Plugins.Booking.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Booking.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.EShop.Plugins.Booking.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Plugins.Booking.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/S6QaezrCRq?label=Discord)](https://discord.gg/S6QaezrCRq)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/EShop?style=social)](https://www.github.com/EasyAbp/EShop)

A booking-business plugin for EShop. It extends EShop to use the [EasyAbp.BookingService](https://github.com/EasyAbp/BookingService) module to help end-users to book some assets online.

## Installation

1. Install the [EasyAbp.BookingService](https://github.com/EasyAbp/BookingService) module locally or remotely.

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.EShop.Orders.Booking.Application (install at EasyAbp.EShop.Orders.Application location)
    * (Optional) EasyAbp.EShop.Payments.Booking.Application (install at EasyAbp.EShop.Payments.Application location)
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

[![](https://mermaid.ink/img/pako:eNrFlU1v2zAMhv-KoMtWIO3uxhCg6wLs1qzBbrkoEp0IlSWPkjZ4Rf_7qI84TpA2LTBgOVmU-JB8RTFPXDoFvOEefkawEr5qsUXRrS2jn4jB2dhtAMv6hwe8ns-_OPeo7XYF-EtLaNj3CDiwDVnFxgALugPWOmTCewi-uB77XBMlwRr2ACGi9awH1E75oziL1c71N_eoAH3D7hBEACZyIEIxlzbYbx12JRITVrEgcEufKYfCmkJG5tLErbb-pmbVsIX1ESn1HRTWp1yE9mNVnzc4_1iPZ1hORzt7G8POof6TF98oBQN4NQ19EuuMfheDn6lkFDCvmcziqDPyLcXQgQ0k4Iq0IZFYXyz5ilLMrONRxtXlH8lVcf9LsLGaUbJqeZNod67rDYQSrCr3Rnr1VK9Ku-_uvNortfdcBPeeJs77S6FP_S4Lei9l7If6YMvdRfNYrLfJSMSrsw_55WSSs7ByYAg-mjBSM2_cfcibB7wwYeLqo5RA56uGLxd0MiqmlzZpbzAeJvRWaPNuNHmCOYBTWWnuILQxjZ_TLgGrXplpZXJ2Q2F98MwHEaK__N574lJ6s0OXzYjBZM4OFJ_xDrCjVqDR_pRwa06p0UzkDX0qaAWpvuZr-0xHY6_oHSyUDg550wpSacbT7F8NVvImYIT9ofr3UE89_wU5QDGI)](https://mermaid-js.github.io/mermaid-live-editor/edit#pako:eNrFlU1v2zAMhv-KoMtWIO3uxhCg6wLs1qzBbrkoEp0IlSWPkjZ4Rf_7qI84TpA2LTBgOVmU-JB8RTFPXDoFvOEefkawEr5qsUXRrS2jn4jB2dhtAMv6hwe8ns-_OPeo7XYF-EtLaNj3CDiwDVnFxgALugPWOmTCewi-uB77XBMlwRr2ACGi9awH1E75oziL1c71N_eoAH3D7hBEACZyIEIxlzbYbx12JRITVrEgcEufKYfCmkJG5tLErbb-pmbVsIX1ESn1HRTWp1yE9mNVnzc4_1iPZ1hORzt7G8POof6TF98oBQN4NQ19EuuMfheDn6lkFDCvmcziqDPyLcXQgQ0k4Iq0IZFYXyz5ilLMrONRxtXlH8lVcf9LsLGaUbJqeZNod67rDYQSrCr3Rnr1VK9Ku-_uvNortfdcBPeeJs77S6FP_S4Lei9l7If6YMvdRfNYrLfJSMSrsw_55WSSs7ByYAg-mjBSM2_cfcibB7wwYeLqo5RA56uGLxd0MiqmlzZpbzAeJvRWaPNuNHmCOYBTWWnuILQxjZ_TLgGrXplpZXJ2Q2F98MwHEaK__N574lJ6s0OXzYjBZM4OFJ_xDrCjVqDR_pRwa06p0UzkDX0qaAWpvuZr-0xHY6_oHSyUDg550wpSacbT7F8NVvImYIT9ofr3UE89_wU5QDGI)

### Admins

1. Define a "booking" product group. Customers can use only these configured product groups for booking.
   ```CSharp
   public override void ConfigureServices(ServiceConfigurationContext context)
   {
       Configure<EShopBookingOptions>(options =>
       {
           options.BookingProductGroups.Add(new BookingProductGroupDefinition("MyBookingProductGroup"));
       });
   }
   ```
2. Use the management pages to create [ProductAsset](https://github.com/EasyAbp/EShop/blob/dev/plugins/Booking/src/EasyAbp.EShop.Plugins.Booking.Domain/EasyAbp/EShop/Plugins/Booking/ProductAssets/ProductAsset.cs) or [ProductAssetCategory](https://github.com/EasyAbp/EShop/blob/dev/plugins/Booking/src/EasyAbp.EShop.Plugins.Booking.Domain/EasyAbp/EShop/Plugins/Booking/ProductAssetCategories/ProductAssetCategory.cs) entities to set prices and more information.

### Customers

1. Use BookingService module's `/api/booking-service/asset-occupancy/search-booking-periods` (GET) or `/api/booking-service/asset-occupancy/search-category-booking-periods` (GET) to get available periods for an asset or an asset category.
2. Create an EShop order with these ExtraProperties:
   * `BookingAssetId` or `BookingAssetCategoryId`.
   * `BookingPeriodSchemeId`, `BookingPeriodId`, `BookingVolume`, `BookingDate`, `BookingStartingTime`, `BookingDuration`.
3. Pay for the order, and then it will automatically call the BookingService module to occupy the asset.
   * If the occupancy succeeds, it will set the order to complete.
   * If the occupancy fails, it will cancel the order and refund the payment.
