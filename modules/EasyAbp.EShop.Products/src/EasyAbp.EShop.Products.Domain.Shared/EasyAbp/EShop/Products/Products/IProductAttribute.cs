using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.Products
{
    public interface IProductAttribute : IHasExtraProperties
    {
        Guid Id { get; }

        [NotNull]
        string DisplayName { get; }

        [CanBeNull]
        string Description { get; }

        int DisplayOrder { get; }

        IEnumerable<IProductAttributeOption> ProductAttributeOptions { get; }
    }
}