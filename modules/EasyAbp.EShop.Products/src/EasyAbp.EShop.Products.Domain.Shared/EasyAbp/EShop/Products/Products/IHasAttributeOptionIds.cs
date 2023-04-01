using System;
using System.Collections.Generic;

namespace EasyAbp.EShop.Products.Products;

public interface IHasAttributeOptionIds
{
    List<Guid> AttributeOptionIds { get; }
}