using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using EasyAbp.BookingService;
using EasyAbp.EShop.Stores;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.GrantedStores.GrantedStore.ViewModels
{
    public class CreateEditGrantedStoreViewModel
    {
        [EasySelector(
            getListedDataSourceUrl: StoresConsts.GetStoreListedDataSourceUrl + "?onlyManageable=true",
            getSingleDataSourceUrl: StoresConsts.GetStoreSingleDataSourceUrl,
            keyPropertyName: "id",
            textPropertyName: "name",
            moduleName: EShopStoresRemoteServiceConsts.ModuleName)]
        [Display(Name = "GrantedStoreStoreId")]
        public Guid StoreId { get; set; }

        [EasySelector(
            getListedDataSourceUrl: BookingServiceUrls.GetAssetListedDataSourceUrl,
            getSingleDataSourceUrl: BookingServiceUrls.GetAssetSingleDataSourceUrl,
            keyPropertyName: "id",
            textPropertyName: "name",
            moduleName: BookingServiceRemoteServiceConsts.ModuleName)]
        [Display(Name = "GrantedStoreAssetId")]
        public Guid? AssetId { get; set; }
        
        [EasySelector(
            getListedDataSourceUrl: BookingServiceUrls.GetAssetCategoryListedDataSourceUrl,
            getSingleDataSourceUrl: BookingServiceUrls.GetAssetCategorySingleDataSourceUrl,
            keyPropertyName: "id",
            textPropertyName: "name",
            moduleName: BookingServiceRemoteServiceConsts.ModuleName)]
        [Display(Name = "GrantedStoreAssetCategoryId")]
        public Guid? AssetCategoryId { get; set; }
        
        [Display(Name = "GrantedStoreAllowAll")]
        public bool AllowAll { get; set; }
    }
}