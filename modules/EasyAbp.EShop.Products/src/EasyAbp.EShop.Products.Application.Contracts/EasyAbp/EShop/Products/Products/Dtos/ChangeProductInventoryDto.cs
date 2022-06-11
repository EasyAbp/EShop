using System;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products.Dtos;

[Serializable]
public class ChangeProductInventoryDto : ExtensibleObject
{
    /// <summary>
    /// Reduce inventory if the value is less than 0
    /// </summary>
    public int ChangedInventory { get; set; }
}