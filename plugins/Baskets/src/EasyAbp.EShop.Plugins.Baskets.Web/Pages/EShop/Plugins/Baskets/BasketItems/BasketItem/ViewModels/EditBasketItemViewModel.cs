using System;

using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Baskets.Web.Pages.EShop.Plugins.Baskets.BasketItems.BasketItem.ViewModels
{
    public class EditBasketItemViewModel
    {
        [Display(Name = "BasketItemQuantity")]
        public int Quantity { get; set; }
    }
}