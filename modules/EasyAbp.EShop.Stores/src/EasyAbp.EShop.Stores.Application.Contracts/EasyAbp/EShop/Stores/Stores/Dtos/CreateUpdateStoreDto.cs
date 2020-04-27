using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Stores.Stores.Dtos
{
    public class CreateUpdateStoreDto
    {
        [Required]
        [DisplayName("StoreName")]
        public string Name { get; set; }
    }
}