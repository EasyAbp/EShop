using System;
using System.Linq;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes;

public static class HasProductScopesExtensions
{
    public static bool IsInScope(this IHasProductScopes scopes, string productGroupName, Guid productId,
        Guid productSkuId)
    {
        return scopes.ProductScopes.Any(x =>
            x.ProductGroupName == productGroupName || x.ProductId == productId || x.ProductSkuId == productSkuId);
    }
}