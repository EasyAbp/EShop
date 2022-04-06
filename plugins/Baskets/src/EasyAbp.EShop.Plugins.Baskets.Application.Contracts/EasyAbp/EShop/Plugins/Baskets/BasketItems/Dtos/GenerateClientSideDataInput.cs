using System.Collections.Generic;
using Volo.Abp.ObjectExtending;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems.Dtos;

public class GenerateClientSideDataInput : ExtensibleObject
{
    public List<GenerateClientSideDataItemInput> Items { get; set; } = new();
}