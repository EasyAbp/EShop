# EShop

[![ABP version](https://img.shields.io/badge/dynamic/xml?style=flat-square&color=yellow&label=abp&query=%2F%2FProject%2FPropertyGroup%2FAbpVersion&url=https%3A%2F%2Fraw.githubusercontent.com%2FEasyAbp%2FEShop%2Fmaster%2FDirectory.Build.props)](https://abp.io)
[![NuGet](https://img.shields.io/nuget/v/EasyAbp.EShop.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.EShop.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Domain.Shared)
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

* Place an Order
    * We have not yet provided relevant UI for this action.
    * Use the API `/api/eShop/orders/order` (POST) to create a new order.

* Pay for the Order
    * We have not yet provided relevant UI for this action.
    * Use the API `/api/eShop/payments/payment` (POST) to create a pending new payment for your order.
        * You can pay for multiple orders at once.
        * You need to decide one of the payment methods provided by the [EasyAbp.PaymentService](https://easyabp.io/modules/PaymentService/) module.
    * Use the API `/api/eShop/orders/order/{id}` (GET) get the order, then you can get the "paymentId" in the result.
    * Use the API `/api/paymentService/payment/{id}/pay` (POST) to complete the payment.
        * "id" is the "paymentId" we got above.
        * Different payment methods require different "extraProperties" for this API. Read the [document](https://easyabp.io/modules/PaymentService/) of the EasyAbp.PaymentService module to learn more.

* Complete the Order
    * We have not yet provided relevant UI for this action.
    * Use the API `/api/paymentService/payment/{id}/pay` (POST) to complete the payment.
        * The customer should have permission to complete the order himself.
        * For some product group, you may want to prohibit users from completing orders themselves, and automatically complete orders through some system rules.

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
