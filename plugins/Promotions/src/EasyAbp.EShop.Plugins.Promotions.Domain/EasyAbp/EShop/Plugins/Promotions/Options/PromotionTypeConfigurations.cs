using System.Collections.Generic;
using System.Linq;
using EasyAbp.EShop.Plugins.Promotions.PromotionTypes;

namespace EasyAbp.EShop.Plugins.Promotions.Options;

public class PromotionTypeConfigurations
{
    private Dictionary<string, PromotionTypeDefinition> PromotionTypes { get; } = new();

    public List<PromotionTypeDefinition> GetAll()
    {
        return PromotionTypes.Values.ToList();
    }

    public PromotionTypeDefinition? GetOrNull(string promotionTypeName)
    {
        return PromotionTypes.TryGetValue(promotionTypeName, out var type) ? type : null;
    }

    public void AddOrUpdate(PromotionTypeDefinition definition)
    {
        PromotionTypes[definition.Name] = definition;
    }

    public void Remove(string promotionTypeName)
    {
        PromotionTypes.Remove(promotionTypeName);
    }
}