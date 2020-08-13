using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.Stores.Dtos
{
    public class CreateUpdateStoreDto : ExtensibleEntityDto
    {
        [Required]
        [DisplayName("StoreName")]
        public string Name { get; set; } 
    }
}