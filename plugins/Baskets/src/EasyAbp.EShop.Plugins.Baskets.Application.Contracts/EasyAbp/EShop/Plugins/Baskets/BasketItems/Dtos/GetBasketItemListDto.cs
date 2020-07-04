using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos
{
    public class GetBasketItemListDto : PagedAndSortedResultRequestDto
    {
        [Required]
        public string BasketName { get; set; }
        
        /// <summary>
        /// Specify the basket item owner user ID. Use current user ID if this property is null.
        /// </summary>
        public Guid? UserId { get; set; }
    }
}