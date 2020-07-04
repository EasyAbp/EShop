using System;

using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Baskets.Web.Pages.EShop.Plugins.Baskets.BasketItems.BasketItem.ViewModels
{
    public class CreateBasketItemViewModel
    {
        [Display(Name = "BasketItemBasketName")]
        public string BasketName { get; set; } = BasketsConsts.DefaultBasketName;
        
        /// <summary>
        /// Specify the basket item owner user ID. Use current user ID if this property is null.
        /// </summary>
        [Display(Name = "BasketItemUserId")]
        public Guid? UserId { get; set; }
        
        [Display(Name = "BasketItemStoreId")]
        public Guid StoreId { get; set; }

        [Display(Name = "BasketItemProductId")]
        public Guid ProductId { get; set; }

        [Display(Name = "BasketItemProductSkuId")]
        public Guid ProductSkuId { get; set; }

        [Display(Name = "BasketItemQuantity")]
        public int Quantity { get; set; }
    }
}