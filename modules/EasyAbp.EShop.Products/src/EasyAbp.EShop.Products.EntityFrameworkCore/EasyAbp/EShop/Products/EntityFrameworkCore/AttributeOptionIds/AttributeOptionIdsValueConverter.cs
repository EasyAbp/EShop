using System;
using System.Collections.Generic;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyAbp.EShop.Products.EntityFrameworkCore.AttributeOptionIds;

public class AttributeOptionIdsValueConverter : ValueConverter<List<Guid>, string>
{
    public AttributeOptionIdsValueConverter() : base(
        v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
        v => JsonSerializer.Deserialize<List<Guid>>(v, (JsonSerializerOptions)null))
    {
    }
}