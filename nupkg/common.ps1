# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

# List of projects
$projects = (

    "modules/EasyAbp.EShop.Baskets/src/EasyAbp.EShop.Baskets.Application",
    "modules/EasyAbp.EShop.Baskets/src/EasyAbp.EShop.Baskets.Application.Contracts",
    "modules/EasyAbp.EShop.Baskets/src/EasyAbp.EShop.Baskets.Domain",
    "modules/EasyAbp.EShop.Baskets/src/EasyAbp.EShop.Baskets.Domain.Shared",
    "modules/EasyAbp.EShop.Baskets/src/EasyAbp.EShop.Baskets.EntityFrameworkCore",
    "modules/EasyAbp.EShop.Baskets/src/EasyAbp.EShop.Baskets.HttpApi",
    "modules/EasyAbp.EShop.Baskets/src/EasyAbp.EShop.Baskets.HttpApi.Client",
    "modules/EasyAbp.EShop.Baskets/src/EasyAbp.EShop.Baskets.MongoDB",
    "modules/EasyAbp.EShop.Baskets/src/EasyAbp.EShop.Baskets.Web",

    "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Application",
    "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Application.Contracts",
    "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Domain",
    "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Domain.Shared",
    "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.EntityFrameworkCore",
    "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.HttpApi",
    "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.HttpApi.Client",
    "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.MongoDB",
    "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Web",

    "modules/EasyAbp.EShop.Payment/src/EasyAbp.EShop.Payment.Application",
    "modules/EasyAbp.EShop.Payment/src/EasyAbp.EShop.Payment.Application.Contracts",
    "modules/EasyAbp.EShop.Payment/src/EasyAbp.EShop.Payment.Domain",
    "modules/EasyAbp.EShop.Payment/src/EasyAbp.EShop.Payment.Domain.Shared",
    "modules/EasyAbp.EShop.Payment/src/EasyAbp.EShop.Payment.EntityFrameworkCore",
    "modules/EasyAbp.EShop.Payment/src/EasyAbp.EShop.Payment.HttpApi",
    "modules/EasyAbp.EShop.Payment/src/EasyAbp.EShop.Payment.HttpApi.Client",
    "modules/EasyAbp.EShop.Payment/src/EasyAbp.EShop.Payment.MongoDB",
    "modules/EasyAbp.EShop.Payment/src/EasyAbp.EShop.Payment.Web",

    "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Application",
    "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Application.Contracts",
    "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Domain",
    "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Domain.Shared",
    "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.EntityFrameworkCore",
    "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.HttpApi",
    "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.HttpApi.Client",
    "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.MongoDB",
    "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Web",

    "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Application",
    "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Application.Contracts",
    "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Domain",
    "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Domain.Shared",
    "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.EntityFrameworkCore",
    "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.HttpApi",
    "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.HttpApi.Client",
    "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.MongoDB",
    "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Web",
)