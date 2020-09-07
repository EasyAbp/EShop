using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels
{
    public class ChangeProductInventoryViewModel
    {
        [Display(Name = "ProductInventoryChangeType")]
        public InventoryChangeType ProductInventoryChangeType { get; set; }
        
        [Display(Name = "ProductInventoryChangedInventory")]
        public int ChangedInventory { get; set; }
    }
}