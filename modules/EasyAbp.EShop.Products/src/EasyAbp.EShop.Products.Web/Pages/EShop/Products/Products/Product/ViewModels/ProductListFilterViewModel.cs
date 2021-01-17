using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using EasyAbp.EShop.Stores;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels
{
    public class ProductListFilterViewModel
    {
        [EasySelector(
            getListedDataSourceUrl: StoresConsts.GetStoreListedDataSourceUrl + "?onlyManageable=true",
            getSingleDataSourceUrl: StoresConsts.GetStoreSingleDataSourceUrl,
            keyPropertyName: "id",
            textPropertyName: "name",
            runScriptOnWindowLoad: true)]
        [Display(Name = "ProductStoreId")]
        public Guid? StoreId { get; set; }
        
        [EasySelector(
            getListedDataSourceUrl: ProductsConsts.GetCategorySummaryListedDataSourceUrl,
            getSingleDataSourceUrl: ProductsConsts.GetCategorySummarySingleDataSourceUrl,
            keyPropertyName: "id",
            textPropertyName: "displayName",
            runScriptOnWindowLoad: true)]
        [Display(Name = "ProductCategoryId")]
        public Guid? CategoryId { get; set; }
    }
}