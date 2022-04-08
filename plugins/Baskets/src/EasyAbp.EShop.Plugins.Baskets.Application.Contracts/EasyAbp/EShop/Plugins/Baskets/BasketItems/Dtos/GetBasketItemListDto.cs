using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    [Serializable]
    public class GetBasketItemListDto
    {
        [Required]
        public string BasketName { get; set; }
        
        /// <summary>
        /// Specify the basket item owner user ID. Use current user ID if this property is null.
        /// </summary>
        public Guid? UserId { get; set; }
    }
}