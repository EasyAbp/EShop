using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Tags.Dtos
{
    public class GetTagListDto : PagedAndSortedResultRequestDto
    {
        [Required]
        public Guid StoreId { get; set; }

        public bool ShowHidden { get; set; }
    }
}