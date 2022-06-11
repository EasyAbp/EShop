using System;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Products.Products.Dtos;

[Serializable]
public class ChangeProductInventoryResultDto : ExtensibleObject
{
    public bool Changed { get; set; }

    public int ChangedInventory { get; set; }

    public int CurrentInventory { get; set; }
}