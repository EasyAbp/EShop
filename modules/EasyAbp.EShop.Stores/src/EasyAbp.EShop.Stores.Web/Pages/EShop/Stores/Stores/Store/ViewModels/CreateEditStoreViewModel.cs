using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store.ViewModels
{
    public class CreateEditStoreViewModel : ExtensibleObject
    {
        [Required]
        [Display(Name = "StoreName")]
        public string Name { get; set; }


        [SelectItems("StoreOwners")]
        [Display(Name = "StoreOwner")]
        public List<Guid> OwnerIds { get; set; }
    }
}