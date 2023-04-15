using System.Collections.Generic;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes;

public interface IHasProductScopes
{
    List<ProductScopeModel> ProductScopes { get; }
}