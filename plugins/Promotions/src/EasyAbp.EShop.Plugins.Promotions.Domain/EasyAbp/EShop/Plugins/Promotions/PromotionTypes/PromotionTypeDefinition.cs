using System;

namespace EasyAbp.EShop.Plugins.Promotions.PromotionTypes;

public class PromotionTypeDefinition
{
    public string Name { get; }

    public Type PromotionHandlerType { get; }

    public PromotionTypeDefinition(string name, Type promotionHandlerType)
    {
        Name = name;
        PromotionHandlerType = promotionHandlerType;
    }
}