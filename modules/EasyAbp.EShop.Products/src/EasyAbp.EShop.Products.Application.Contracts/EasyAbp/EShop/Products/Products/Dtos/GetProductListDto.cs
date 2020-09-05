﻿using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Products.Dtos
{
    [Serializable]
    public class GetProductListDto : PagedAndSortedResultRequestDto
    {
        [Required]
        public Guid StoreId { get; set; }
        
        public Guid? CategoryId { get; set; }
        
        public bool ShowHidden { get; set; }
    }
}