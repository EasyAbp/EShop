using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using EasyAbp.BookingService.AssetOccupancies;
using EasyAbp.BookingService.AssetOccupancies.Dtos;
using EasyAbp.BookingService.AssetOccupancyProviders;
using EasyAbp.BookingService.PeriodSchemes;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Plugins.Booking.BookingProductGroupDefinitions;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Plugins.Booking.GrantedStores;
using EasyAbp.EShop.Plugins.Booking.GrantedStores.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Booking.Authorization
{
    public class BookingOrderCreationAuthorizationHandler : OrderCreationAuthorizationHandler
    {
        private readonly IPeriodSchemeAppService _periodSchemeAppService;
        private readonly IProductAssetAppService _productAssetAppService;
        private readonly IGrantedStoreAppService _grantedStoreAppService;
        private readonly IProductAssetCategoryAppService _productAssetCategoryAppService;
        private readonly IAssetOccupancyAppService _assetOccupancyAppService;
        private readonly IBookingProductGroupDefinitionAppService _definitionAppService;

        public BookingOrderCreationAuthorizationHandler(
            IPeriodSchemeAppService periodSchemeAppService,
            IProductAssetAppService productAssetAppService,
            IGrantedStoreAppService grantedStoreAppService,
            IProductAssetCategoryAppService productAssetCategoryAppService,
            IAssetOccupancyAppService assetOccupancyAppService,
            IBookingProductGroupDefinitionAppService definitionAppService)
        {
            _periodSchemeAppService = periodSchemeAppService;
            _productAssetAppService = productAssetAppService;
            _grantedStoreAppService = grantedStoreAppService;
            _productAssetCategoryAppService = productAssetCategoryAppService;
            _assetOccupancyAppService = assetOccupancyAppService;
            _definitionAppService = definitionAppService;
        }

        protected override async Task HandleOrderCreationAsync(AuthorizationHandlerContext context,
            OrderOperationAuthorizationRequirement requirement, OrderCreationResource resource)
        {
            var productGroupNames = (await _definitionAppService.GetListAsync()).Items.Select(x => x.ProductGroupName);

            var bookingOrderLines = resource.Input.OrderLines.Where(x =>
                productGroupNames.Contains(resource.ProductDictionary[x.ProductId].ProductGroupName)).ToList();

            if (!bookingOrderLines.Any())
            {
                return;
            }

            var models = new List<OccupyAssetInfoModel>();
            var byCategoryModels = new List<OccupyAssetByCategoryInfoModel>();

            foreach (var orderLine in bookingOrderLines)
            {
                if (!await IsPeriodInfoValidAsync(orderLine))
                {
                    context.Fail();
                    return;
                }

                var assetId = orderLine.FindBookingAssetId();
                var assetCategoryId = orderLine.FindBookingAssetCategoryId();

                if (assetId is not null)
                {
                    if (!await IsAssetInfoValidAsync(orderLine, resource))
                    {
                        context.Fail();
                        return;
                    }

                    models.Add(CreateOccupyAssetInfoModel(assetId.Value, orderLine));
                } 
                else if (assetCategoryId is not null)
                {
                    if (!await IsAssetCategoryInfoValidAsync(orderLine, resource))
                    {
                        context.Fail();
                        return;
                    }
                    
                    byCategoryModels.Add(CreateOccupyAssetByCategoryInfoModel(assetCategoryId.Value, orderLine));
                }
                else
                {
                    context.Fail();
                    return;
                }
            }

            try
            {
                await _assetOccupancyAppService.CheckBulkCreateAsync(new BulkCreateAssetOccupancyDto
                {
                    OccupierUserId = Check.NotNull(context.User.FindUserId(), "CurrentUserId"),
                    Models = models,
                    ByCategoryModels = byCategoryModels
                });
            }
            catch
            {
                context.Fail();
                return;
            }
        }

        protected virtual OccupyAssetInfoModel CreateOccupyAssetInfoModel(Guid assetId, ICreateOrderLineInfo orderLine)
        {
            return new OccupyAssetInfoModel(
                assetId,
                orderLine.GetBookingVolume(),
                orderLine.GetBookingDate(),
                orderLine.GetBookingStartingTime(),
                orderLine.GetBookingDuration()
            );
        }

        protected virtual OccupyAssetByCategoryInfoModel CreateOccupyAssetByCategoryInfoModel(Guid assetCategoryId,
            ICreateOrderLineInfo orderLine)
        {
            return new OccupyAssetByCategoryInfoModel(
                assetCategoryId,
                orderLine.FindBookingPeriodSchemeId(),
                orderLine.GetBookingVolume(),
                orderLine.GetBookingDate(),
                orderLine.GetBookingStartingTime(),
                orderLine.GetBookingDuration()
            );
        }
        
        protected virtual async Task<bool> IsAssetInfoValidAsync(ICreateOrderLineInfo orderLine,
            OrderCreationResource resource)
        {
            var mapping = (await _grantedStoreAppService.GetListAsync(new GetGrantedStoreListDto
            {
                MaxResultCount = 1,
                StoreId = resource.Input.StoreId,
                AssetId = orderLine.GetBookingAssetId()
            })).Items.FirstOrDefault();

            if (mapping is null)
            {
                mapping = (await _grantedStoreAppService.GetListAsync(new GetGrantedStoreListDto
                {
                    MaxResultCount = 1,
                    AllowAll = true
                })).Items.FirstOrDefault();
            }

            if (mapping is null)
            {
                return false;
            }

            var productAsset = (await _productAssetAppService.GetListAsync(
                new GetProductAssetListDto
                {
                    MaxResultCount = 1,
                    StoreId = resource.Input.StoreId,
                    ProductId = orderLine.ProductId,
                    ProductSkuId = orderLine.ProductSkuId,
                    AssetId = orderLine.GetBookingAssetId(),
                    PeriodSchemeId = orderLine.GetBookingPeriodSchemeId()
                }
            )).Items.FirstOrDefault();

            return productAsset is not null;
        }

        protected virtual async Task<bool> IsAssetCategoryInfoValidAsync(ICreateOrderLineInfo orderLine,
            OrderCreationResource resource)
        {
            var mapping = (await _grantedStoreAppService.GetListAsync(new GetGrantedStoreListDto
            {
                MaxResultCount = 1,
                StoreId = resource.Input.StoreId,
                AssetCategoryId = orderLine.GetBookingAssetCategoryId()
            })).Items.FirstOrDefault();

            if (mapping is null)
            {
                mapping = (await _grantedStoreAppService.GetListAsync(new GetGrantedStoreListDto
                {
                    MaxResultCount = 1,
                    AllowAll = true
                })).Items.FirstOrDefault();
            }

            if (mapping is null)
            {
                return false;
            }
            
            var productAssetCategory = (await _productAssetCategoryAppService.GetListAsync(
                new GetProductAssetCategoryListDto
                {
                    MaxResultCount = 1,
                    StoreId = resource.Input.StoreId,
                    ProductId = orderLine.ProductId,
                    ProductSkuId = orderLine.ProductSkuId,
                    AssetCategoryId = orderLine.GetBookingAssetCategoryId(),
                    PeriodSchemeId = orderLine.GetBookingPeriodSchemeId()
                }
            )).Items.FirstOrDefault();

            return productAssetCategory is not null;
        }
        
        protected virtual async Task<bool> IsPeriodInfoValidAsync(ICreateOrderLineInfo orderLine)
        {
            var periodSchemeId = orderLine.GetBookingPeriodSchemeId();
            var periodId = orderLine.GetBookingPeriodId();

            var periodScheme = await _periodSchemeAppService.GetAsync(periodSchemeId);
            var period = periodScheme.Periods.Find(x => x.Id == periodId);

            return period is not null;
        }
    }
}
