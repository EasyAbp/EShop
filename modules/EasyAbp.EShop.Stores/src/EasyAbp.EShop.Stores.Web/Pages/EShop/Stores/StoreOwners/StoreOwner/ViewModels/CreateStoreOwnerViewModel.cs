using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.Abp.TagHelperPlus.EasySelector;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner.ViewModels
{
    public class CreateEditStoreOwnerViewModel
    {
        [EasySelector(
            getListedDataSourceUrl: StoresConsts.GetStoreListedDataSourceUrl + "?onlyManageable=true",
            getSingleDataSourceUrl: StoresConsts.GetStoreSingleDataSourceUrl,
            keyPropertyName: "id",
            textPropertyName: "name")]
        [Display(Name = "StoreOwnerStoreId")]
        public Guid StoreId { get; set; }

        [EasySelector(
            getListedDataSourceUrl: "/api/identity/users",
            getSingleDataSourceUrl: "/api/identity/users/{id}",
            keyPropertyName: "id",
            textPropertyName: "name",
            alternativeTextPropertyName: "userName")]
        [Display(Name = "StoreOwnerOwnerUserId")]
        public Guid OwnerUserId  { get; set; }
    }
}