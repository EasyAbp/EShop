using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Categories.Dtos
{
    public class CategorySummaryDto : EntityDto<Guid>
    {
        public string UniqueName { get; set; }
        
        public string DisplayName { get; set; }
        
        public string TreedDisplayName { get; set; }
        
        public int Level { get; set; }
        
        public Guid? ParentId { get; set; }
    }
}