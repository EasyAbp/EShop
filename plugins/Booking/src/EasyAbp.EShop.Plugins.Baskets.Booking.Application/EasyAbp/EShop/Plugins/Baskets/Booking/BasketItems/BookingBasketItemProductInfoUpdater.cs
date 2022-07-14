using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.BookingService.AssetOccupancies;
using EasyAbp.BookingService.AssetOccupancies.Dtos;
using EasyAbp.EShop.Plugins.Baskets.BasketItems;
using EasyAbp.EShop.Plugins.Baskets.Booking.ObjectExtending;
using EasyAbp.EShop.Plugins.Booking.BookingProductGroupDefinitions;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.Extensions.Caching.Distributed;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Plugins.Baskets.Booking.BasketItems;

public class BookingBasketItemProductInfoUpdater : IBasketItemProductInfoUpdater, ITransientDependency
{
    protected List<string> ProductGroupNames { get; set; }
    protected bool? AllowedToUpdate { get; set; }

    protected ICurrentUser CurrentUser { get; }
    protected IDistributedCache<BookingBasketItemUpdatedCacheItem> Cache { get; }
    protected IAssetOccupancyAppService AssetOccupancyAppService { get; }
    protected IBookingProductGroupDefinitionAppService DefinitionAppService { get; }

    public BookingBasketItemProductInfoUpdater(
        ICurrentUser currentUser,
        IDistributedCache<BookingBasketItemUpdatedCacheItem> cache,
        IAssetOccupancyAppService assetOccupancyAppService,
        IBookingProductGroupDefinitionAppService definitionAppService)
    {
        CurrentUser = currentUser;
        Cache = cache;
        AssetOccupancyAppService = assetOccupancyAppService;
        DefinitionAppService = definitionAppService;
    }

    public virtual async Task UpdateProductDataAsync(int targetQuantity, IBasketItem item, ProductDto productDto)
    {
        if (!await IsAllowedToUpdateAsync(item, productDto) ||
            !await IsBookingProductGroupAsync(productDto.ProductGroupName))
        {
            return;
        }

        var assetId = item.FindBookingAssetId();
        var assetCategoryId = item.FindBookingAssetCategoryId();

        if (assetId is not null)
        {
            try
            {
                await AssetOccupancyAppService.CheckCreateAsync(new CreateAssetOccupancyDto
                {
                    AssetId = assetId.Value,
                    Volume = item.GetBookingVolume(),
                    Date = item.GetBookingDate(),
                    StartingTime = item.GetBookingStartingTime(),
                    Duration = item.GetBookingDuration()
                });
            }
            catch
            {
                item.SetIsInvalid(true);
            }
        }
        else
        {
            try
            {
                await AssetOccupancyAppService.CheckCreateByCategoryIdAsync(new CreateAssetOccupancyByCategoryIdDto
                {
                    AssetCategoryId = assetCategoryId!.Value,
                    Volume = item.GetBookingVolume(),
                    Date = item.GetBookingDate(),
                    StartingTime = item.GetBookingStartingTime(),
                    Duration = item.GetBookingDuration()
                });
            }
            catch
            {
                item.SetIsInvalid(true);
            }
        }

    }

    protected virtual async Task<bool> IsAllowedToUpdateAsync(IBasketItem item, ProductDto productDto)
    {
        if (AllowedToUpdate.HasValue)
        {
            return AllowedToUpdate.Value;
        }

        var key = GetCacheItemKey(item, productDto);

        AllowedToUpdate = await Cache.GetAsync(key) is null;

        await Cache.SetAsync(key, new BookingBasketItemUpdatedCacheItem(), new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30)
        });

        return AllowedToUpdate.Value;
    }

    protected virtual string GetCacheItemKey(IBasketItem item, ProductDto productDto)
    {
        return CurrentUser.GetId().ToString();
    }

    protected virtual async Task<bool> IsBookingProductGroupAsync(string productGroupName)
    {
        ProductGroupNames ??=
            (await DefinitionAppService.GetListAsync()).Items.Select(x => x.ProductGroupName).ToList();

        return ProductGroupNames.Contains(productGroupName);
    }
}