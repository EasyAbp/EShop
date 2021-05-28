using System;
using System.ComponentModel;
using EasyAbp.EShop.Inventory.Inventories;

namespace EasyAbp.EShop.Inventory.Suppliers.Dtos
{
    [Serializable]
    public class SupplierCreateDto
    {
        [DisplayName("SupplierName")]
        public string Name { get; set; }

        [DisplayName("SupplierAddress")]
        public Address Address { get; set; }

        [DisplayName("SupplierDescription")]
        public string Description { get; set; }

        [DisplayName("SupplierContactName")]
        public string ContactName { get; set; }

        [DisplayName("SupplierContactPhoneNumber")]
        public string ContactPhoneNumber { get; set; }

        [DisplayName("SupplierBusinessScope")]
        public string BusinessScope { get; set; }

    }
}