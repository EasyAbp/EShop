# EShop

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FEShop%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.EShop.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.EShop.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Domain.Shared)
[![Discord online](https://badgen.net/discord/online-members/S6QaezrCRq?label=Discord)](https://discord.gg/S6QaezrCRq)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/EShop?style=social)](https://www.github.com/EasyAbp/EShop)

An abp application module group that provides basic e-shop service.

## Online Demo

We have launched an online demo for this module: [https://eshop.samples.easyabp.io](https://eshop.samples.easyabp.io)

## Installation

1. Follow [the document](https://github.com/EasyAbp/PaymentService#installation) to install the dependent PaymentService module.

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-nuget-packages))

    * EasyAbp.EShop.Application
    * EasyAbp.EShop.Application.Contracts
    * EasyAbp.EShop.Domain
    * EasyAbp.EShop.Domain.Shared
    * EasyAbp.EShop.EntityFrameworkCore
    * EasyAbp.EShop.HttpApi
    * EasyAbp.EShop.HttpApi.Client
    * (Optional) EasyAbp.EShop.MongoDB
    * (Optional) EasyAbp.EShop.Web

    > The above packages are integration packages containing the necessary sub-modules.
    > Please install packages of each sub-module separately if you are using microservices.
    > For example: install only the `EasyAbp.EShop.Products.Application` package.

1. Add `DependsOn(typeof(EShopXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/docs/How-To.md#add-module-dependencies))

1. Add `builder.ConfigureEShop();` to the `OnModelCreating()` method in **MyProjectMigrationsDbContext.cs**.

1. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC&DB=EF#add-database-migration).

## Basic Usage

[![](https://mermaid.ink/img/pako:eNqtVMFu2zAM_RVCl21A2g8wily6HHrZumW7-SJbTC3AklyJWmAU_fdRsjynmdN2wHyyKOo98j2JT6J1CkUlAj5GtC1-1vLBS1PbmmQk9yWaBn1tgb-fAf3Vdrvbd264vvdOxZZCBd8i-hGoQximGPQ6EJCDgAjHThLw3lFaChPOS4ArhkzIFXxHit6Gv6BW2L96hZ65bz1KQpAWXIok0iaOEJxB6rR9OCWczix0eQ1tRlAb8IU9h-_UWstyNGhTy3uSnkDCMEXg4HyuOhfxoslyZGEtkZn3td6cJW2ji6Ef4fGPyJnjpvHbyNv9EvoQZuw7BZ0MXN4v2UdcLeeca8dYNE5qqh25m7xd0nm9XRHyHGOPdFLBRUnObTh1fTLxqKlbkF414taZoUe-AfnOFGmb8fz0m2YUmP9th1Y_tMF_d-NE_LnD5Mo7TZhUHJidH4aCITb8irrpYqea1pHeNuRSNxfkKs6YcdHooz6kWaAD8Iyx3FR6sLP40HE8p4ZP73i3sy5iIwx6w9XxFHtKB2vBpRusRcW_Cg8y9lSL2j5zahwUX_Gd0uS8qA6yD7gRadDtR9uKinzEOalMwpL1_Bt1vcxC)](https://mermaid-js.github.io/mermaid-live-editor/edit#pako:eNqtVMFu2zAM_RVCl21A2g8wily6HHrZumW7-SJbTC3AklyJWmAU_fdRsjynmdN2wHyyKOo98j2JT6J1CkUlAj5GtC1-1vLBS1PbmmQk9yWaBn1tgb-fAf3Vdrvbd264vvdOxZZCBd8i-hGoQximGPQ6EJCDgAjHThLw3lFaChPOS4ArhkzIFXxHit6Gv6BW2L96hZ65bz1KQpAWXIok0iaOEJxB6rR9OCWczix0eQ1tRlAb8IU9h-_UWstyNGhTy3uSnkDCMEXg4HyuOhfxoslyZGEtkZn3td6cJW2ji6Ef4fGPyJnjpvHbyNv9EvoQZuw7BZ0MXN4v2UdcLeeca8dYNE5qqh25m7xd0nm9XRHyHGOPdFLBRUnObTh1fTLxqKlbkF414taZoUe-AfnOFGmb8fz0m2YUmP9th1Y_tMF_d-NE_LnD5Mo7TZhUHJidH4aCITb8irrpYqea1pHeNuRSNxfkKs6YcdHooz6kWaAD8Iyx3FR6sLP40HE8p4ZP73i3sy5iIwx6w9XxFHtKB2vBpRusRcW_Cg8y9lSL2j5zahwUX_Gd0uS8qA6yD7gRadDtR9uKinzEOalMwpL1_Bt1vcxC)

* Create a Store (optional)
    * EShop supports multi-store, it provides a [default store](https://github.com/EasyAbp/EShop/blob/master/modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Domain/EasyAbp/EShop/Stores/Stores/StoreDataSeeder.cs), it will be created when you seed the initial data. (learn more about ABP [Data Seeding](https://docs.abp.io/en/abp/latest/Data-Seeding))
    * Use the store management page to create a new store.

* Define a Product Group (optional)
    * Product group is used to classify different types of products, so we can customize different behavior for them, for example, products of the "GiftCard" product group could automatically send the card number and password to the customer's mailbox.
    * EShop provides a [default product group](https://github.com/EasyAbp/EShop/blob/master/modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Domain/EasyAbp/EShop/Products/EShopProductsDomainModule.cs#L29-L36).
    * Refer to the configuration of the default product group and define a new product group.

* Create a Product
    * Use the product management page to create new a product.
    * Click the "SKU" item in the actions button of the product you created and then create an SKU.

* Place an Order (We have not yet provided UI for this step.)
    * Use the API `/api/e-shop/orders/order` (POST) to create a new order.

* Pay for the Order (We have not yet provided UI for this step.)
    * Wait for the inventory reduction to be completed, get the order and ensure the Order.ReducedInventoryAfterPlacingTime is not null.
    * Use the API `/api/e-shop/payments/payment` (POST) to create a pending new payment for your order.
        * You can pay for multiple orders at once.
        * You need to decide one of the payment methods provided by the [EasyAbp.PaymentService](https://easyabp.io/modules/PaymentService/) module.
    * Use the API `/api/e-shop/orders/order/{id}` (GET) get the order, then you can get the "paymentId" in the result.
    * Use the API `/api/payment-service/payment/{id}/pay` (POST) to complete the payment.
        * "id" is the "paymentId" we got above.
        * Different payment methods require different "extraProperties" for this API. Read the [document](https://easyabp.io/modules/PaymentService/) of the EasyAbp.PaymentService module to learn more.

* Complete the Order
    * We have not yet provided relevant UI for this action.
    * Use the API `/api/e-shop/orders/order/{id}/complete` (POST) to complete the order.
        * The customer should have permission to complete the order himself.
        * You need to override the "CompleteAsync" method of the "OrderAppService" if you want to prohibit users from completing orders themselves for some specific product groups but complete them through automated processes.

## Advanced Usages

We can customize some features to use EShop in complex application scenarios.

### Gift Card Shop

* When a gift card order is paid, automatically send the card number and password to the customer's mailbox.
* Automatically set the order status to completed after the mail is sent.
* Read the article to learn how to implement it. (todo)

### Paid Knowledge Market
* Also use the [EasyAbp.SharedResources](https://easyabp.io/modules/SharedResources/) module.
* The carrier of knowledge can be articles, pictures, audios, videos, files, streams, etc.
* When a knowledge order is paid, automatically authorize the customer to access the resources he purchased.
* Read the article to learn how to implement it. (todo)

## Submodules

* Core modules
  * Orders
  * Payments
  * Plugins
  * Products
  * Stores

* Plugin modules
  * Baskets
  * Coupons

## Roadmap

Todo.
