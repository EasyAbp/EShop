#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["samples/EShopSample/aspnet-core/src/EShopSample.Web/EShopSample.Web.csproj", "samples/EShopSample/aspnet-core/src/EShopSample.Web/"]
COPY ["integration/EasyAbp.EShop/src/EasyAbp.EShop.Web/EasyAbp.EShop.Web.csproj", "integration/EasyAbp.EShop/src/EasyAbp.EShop.Web/"]
COPY ["modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.Web/EasyAbp.EShop.Payments.Web.csproj", "modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.Web/"]
COPY ["modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Application.Contracts/EasyAbp.EShop.Stores.Application.Contracts.csproj", "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Application.Contracts/"]
COPY ["modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Domain.Shared/EasyAbp.EShop.Stores.Domain.Shared.csproj", "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Domain.Shared/"]
COPY ["modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.HttpApi/EasyAbp.EShop.Payments.HttpApi.csproj", "modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.HttpApi/"]
COPY ["modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.Application.Contracts/EasyAbp.EShop.Payments.Application.Contracts.csproj", "modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.Application.Contracts/"]
COPY ["modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.Domain.Shared/EasyAbp.EShop.Payments.Domain.Shared.csproj", "modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.Domain.Shared/"]
COPY ["modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.Web/EasyAbp.EShop.Plugins.Web.csproj", "modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.Web/"]
COPY ["modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.HttpApi/EasyAbp.EShop.Plugins.HttpApi.csproj", "modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.HttpApi/"]
COPY ["modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.Application.Contracts/EasyAbp.EShop.Plugins.Application.Contracts.csproj", "modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.Application.Contracts/"]
COPY ["modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.Domain.Shared/EasyAbp.EShop.Plugins.Domain.Shared.csproj", "modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.Domain.Shared/"]
COPY ["integration/EasyAbp.EShop/src/EasyAbp.EShop.HttpApi/EasyAbp.EShop.HttpApi.csproj", "integration/EasyAbp.EShop/src/EasyAbp.EShop.HttpApi/"]
COPY ["integration/EasyAbp.EShop/src/EasyAbp.EShop.Application.Contracts/EasyAbp.EShop.Application.Contracts.csproj", "integration/EasyAbp.EShop/src/EasyAbp.EShop.Application.Contracts/"]
COPY ["modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Application.Contracts/EasyAbp.EShop.Products.Application.Contracts.csproj", "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Application.Contracts/"]
COPY ["modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Domain.Shared/EasyAbp.EShop.Products.Domain.Shared.csproj", "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Domain.Shared/"]
COPY ["modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Application.Contracts/EasyAbp.EShop.Orders.Application.Contracts.csproj", "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Application.Contracts/"]
COPY ["modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Domain.Shared/EasyAbp.EShop.Orders.Domain.Shared.csproj", "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Domain.Shared/"]
COPY ["integration/EasyAbp.EShop/src/EasyAbp.EShop.Domain.Shared/EasyAbp.EShop.Domain.Shared.csproj", "integration/EasyAbp.EShop/src/EasyAbp.EShop.Domain.Shared/"]
COPY ["modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.HttpApi/EasyAbp.EShop.Stores.HttpApi.csproj", "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.HttpApi/"]
COPY ["modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.HttpApi/EasyAbp.EShop.Orders.HttpApi.csproj", "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.HttpApi/"]
COPY ["modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.HttpApi/EasyAbp.EShop.Products.HttpApi.csproj", "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.HttpApi/"]
COPY ["modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Web/EasyAbp.EShop.Orders.Web.csproj", "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Web/"]
COPY ["modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Web/EasyAbp.EShop.Stores.Web.csproj", "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Web/"]
COPY ["modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Web/EasyAbp.EShop.Products.Web.csproj", "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Web/"]
COPY ["samples/EShopSample/aspnet-core/src/EShopSample.Application/EShopSample.Application.csproj", "samples/EShopSample/aspnet-core/src/EShopSample.Application/"]
COPY ["integration/EasyAbp.EShop/src/EasyAbp.EShop.Application/EasyAbp.EShop.Application.csproj", "integration/EasyAbp.EShop/src/EasyAbp.EShop.Application/"]
COPY ["integration/EasyAbp.EShop/src/EasyAbp.EShop.Domain/EasyAbp.EShop.Domain.csproj", "integration/EasyAbp.EShop/src/EasyAbp.EShop.Domain/"]
COPY ["modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.Domain/EasyAbp.EShop.Plugins.Domain.csproj", "modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.Domain/"]
COPY ["modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Domain/EasyAbp.EShop.Orders.Domain.csproj", "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Domain/"]
COPY ["modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Domain/EasyAbp.EShop.Products.Domain.csproj", "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Domain/"]
COPY ["modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.Domain/EasyAbp.EShop.Payments.Domain.csproj", "modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.Domain/"]
COPY ["modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Domain/EasyAbp.EShop.Stores.Domain.csproj", "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Domain/"]
COPY ["modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Application/EasyAbp.EShop.Stores.Application.csproj", "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.Application/"]
COPY ["modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Application/EasyAbp.EShop.Products.Application.csproj", "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.Application/"]
COPY ["modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.Application/EasyAbp.EShop.Plugins.Application.csproj", "modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.Application/"]
COPY ["modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.Application/EasyAbp.EShop.Payments.Application.csproj", "modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.Application/"]
COPY ["modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Application/EasyAbp.EShop.Orders.Application.csproj", "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.Application/"]
COPY ["samples/EShopSample/aspnet-core/src/EShopSample.Domain/EShopSample.Domain.csproj", "samples/EShopSample/aspnet-core/src/EShopSample.Domain/"]
COPY ["plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Domain/EasyAbp.EShop.Plugins.Baskets.Domain.csproj", "plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Domain/"]
COPY ["plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Domain.Shared/EasyAbp.EShop.Plugins.Baskets.Domain.Shared.csproj", "plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Domain.Shared/"]
COPY ["samples/EShopSample/aspnet-core/src/EShopSample.Domain.Shared/EShopSample.Domain.Shared.csproj", "samples/EShopSample/aspnet-core/src/EShopSample.Domain.Shared/"]
COPY ["samples/EShopSample/aspnet-core/src/EShopSample.Application.Contracts/EShopSample.Application.Contracts.csproj", "samples/EShopSample/aspnet-core/src/EShopSample.Application.Contracts/"]
COPY ["plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Application.Contracts/EasyAbp.EShop.Plugins.Baskets.Application.Contracts.csproj", "plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Application.Contracts/"]
COPY ["plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Application/EasyAbp.EShop.Plugins.Baskets.Application.csproj", "plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Application/"]
COPY ["plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Web/EasyAbp.EShop.Plugins.Baskets.Web.csproj", "plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.Web/"]
COPY ["plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.HttpApi/EasyAbp.EShop.Plugins.Baskets.HttpApi.csproj", "plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.HttpApi/"]
COPY ["samples/EShopSample/aspnet-core/src/EShopSample.EntityFrameworkCore.DbMigrations/EShopSample.EntityFrameworkCore.DbMigrations.csproj", "samples/EShopSample/aspnet-core/src/EShopSample.EntityFrameworkCore.DbMigrations/"]
COPY ["samples/EShopSample/aspnet-core/src/EShopSample.EntityFrameworkCore/EShopSample.EntityFrameworkCore.csproj", "samples/EShopSample/aspnet-core/src/EShopSample.EntityFrameworkCore/"]
COPY ["integration/EasyAbp.EShop/src/EasyAbp.EShop.EntityFrameworkCore/EasyAbp.EShop.EntityFrameworkCore.csproj", "integration/EasyAbp.EShop/src/EasyAbp.EShop.EntityFrameworkCore/"]
COPY ["modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.EntityFrameworkCore/EasyAbp.EShop.Stores.EntityFrameworkCore.csproj", "modules/EasyAbp.EShop.Stores/src/EasyAbp.EShop.Stores.EntityFrameworkCore/"]
COPY ["modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.EntityFrameworkCore/EasyAbp.EShop.Products.EntityFrameworkCore.csproj", "modules/EasyAbp.EShop.Products/src/EasyAbp.EShop.Products.EntityFrameworkCore/"]
COPY ["modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.EntityFrameworkCore/EasyAbp.EShop.Orders.EntityFrameworkCore.csproj", "modules/EasyAbp.EShop.Orders/src/EasyAbp.EShop.Orders.EntityFrameworkCore/"]
COPY ["modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.EntityFrameworkCore/EasyAbp.EShop.Payments.EntityFrameworkCore.csproj", "modules/EasyAbp.EShop.Payments/src/EasyAbp.EShop.Payments.EntityFrameworkCore/"]
COPY ["modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.EntityFrameworkCore/EasyAbp.EShop.Plugins.EntityFrameworkCore.csproj", "modules/EasyAbp.EShop.Plugins/src/EasyAbp.EShop.Plugins.EntityFrameworkCore/"]
COPY ["plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore/EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore.csproj", "plugins/Baskets/src/EasyAbp.EShop.Plugins.Baskets.EntityFrameworkCore/"]
COPY ["samples/EShopSample/aspnet-core/src/EShopSample.HttpApi/EShopSample.HttpApi.csproj", "samples/EShopSample/aspnet-core/src/EShopSample.HttpApi/"]
RUN dotnet restore "samples/EShopSample/aspnet-core/src/EShopSample.Web/EShopSample.Web.csproj"
COPY . .
WORKDIR "/src/samples/EShopSample/aspnet-core/src/EShopSample.Web"
RUN dotnet build "EShopSample.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EShopSample.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EShopSample.Web.dll"]