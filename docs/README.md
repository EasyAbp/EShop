# EShop

[![NuGet](https://img.shields.io/nuget/v/EasyAbp.EShop.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Domain.Shared)
[![NuGet Download](https://img.shields.io/nuget/dt/EasyAbp.EShop.Domain.Shared.svg?style=flat-square)](https://www.nuget.org/packages/EasyAbp.EShop.Domain.Shared)
[![GitHub stars](https://img.shields.io/github/stars/EasyAbp/EShop?style=social)](https://www.github.com/EasyAbp/EShop)

An abp application module group that provides basic e-shop service.

## Online Demo

We have launched an online demo for this module: [https://eshop.samples.easyabp.io](https://eshop.samples.easyabp.io)

## Installation

1. Follow [the document](https://github.com/EasyAbp/PaymentService#installation) to install the dependent PaymentService module.

1. Install the following NuGet packages. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/How-To.md#add-nuget-packages))

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

1. Add `DependsOn(typeof(EShopXxxModule))` attribute to configure the module dependencies. ([see how](https://github.com/EasyAbp/EasyAbpGuide/blob/master/How-To.md#add-module-dependencies))

1. Add `builder.ConfigureEShop();` to the `OnModelCreating()` method in **MyProjectMigrationsDbContext.cs**.

1. Add EF Core migrations and update your database. See: [ABP document](https://docs.abp.io/en/abp/latest/Tutorials/Part-1?UI=MVC#add-new-migration-update-the-database).

## Usage

Todo.

## Sub-Modules

* Core modules
  * Orders
  * Payments
  * Plugins
  * Products
  * Stores

* Plugin modules
  * Baskets

## Roadmap

Todo.
