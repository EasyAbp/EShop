using System;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions.Dtos;

[Serializable]
public class PromotionTypeDto
{
    public string Name { get; set; }

    public string DisplayName { get; set; }

    public string ConfigurationsTemplate { get; set; }

    public PromotionTypeDto(string name, string displayName, string configurationsTemplate)
    {
        Name = name;
        DisplayName = displayName;
        ConfigurationsTemplate = configurationsTemplate;
    }
}