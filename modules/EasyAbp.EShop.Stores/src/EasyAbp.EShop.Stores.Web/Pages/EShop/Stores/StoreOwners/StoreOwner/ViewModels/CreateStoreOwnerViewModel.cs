using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner.ViewModels
{
    public class CreateEditStoreOwnerViewModel : ExtensibleObject
    {
        [Required]
        public Guid StoreId { get; set; }


        [Required]
        public Guid MultiStoreCrudAppService  { get; set; }
    }
}