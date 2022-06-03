using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using EasyAbp.BookingService;
using EasyAbp.EShop.Stores;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.StoreAssetCategories.StoreAssetCategory.ViewModels
{
    public class CreateEditStoreAssetCategoryViewModel
    {
        [EasySelector(
            getListedDataSourceUrl: StoresConsts.GetStoreListedDataSourceUrl + "?onlyManageable=true",
            getSingleDataSourceUrl: StoresConsts.GetStoreSingleDataSourceUrl,
            keyPropertyName: "id",
            textPropertyName: "name",
            moduleName: EShopStoresRemoteServiceConsts.ModuleName)]
        [Display(Name = "StoreAssetCategoryStoreId")]
        public Guid StoreId { get; set; }

        [EasySelector(
            getListedDataSourceUrl: BookingServiceUrls.GetAssetCategoryListedDataSourceUrl,
            getSingleDataSourceUrl: BookingServiceUrls.GetAssetCategorySingleDataSourceUrl,
            keyPropertyName: "id",
            textPropertyName: "name",
            moduleName: BookingServiceRemoteServiceConsts.ModuleName)]
        [Display(Name = "StoreAssetCategoryAssetCategoryId")]
        public Guid AssetCategoryId { get; set; }
    }
}