using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using EasyAbp.EShop.Products;
using EasyAbp.EShop.Stores;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset.ViewModels
{
    public class ProductAssetListFilterViewModel
    {
        [EasySelector(
            getListedDataSourceUrl: StoresConsts.GetStoreListedDataSourceUrl + "?onlyManageable=true",
            getSingleDataSourceUrl: StoresConsts.GetStoreSingleDataSourceUrl,
            keyPropertyName: "id",
            textPropertyName: "name",
            runScriptOnWindowLoad: true)]
        [Display(Name = "ProductAssetStoreId")]
        public Guid? StoreId { get; set; }
    }
}