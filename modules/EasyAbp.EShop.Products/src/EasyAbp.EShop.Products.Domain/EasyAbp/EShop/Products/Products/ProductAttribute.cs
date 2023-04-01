using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.DynamicProxy;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttribute : FullAuditedEntity<Guid>, IProductAttribute
    {
        public virtual string DisplayName { get; protected set; }

        public virtual string Description { get; protected set; }

        public virtual int DisplayOrder { get; protected set; }

        public ExtraPropertyDictionary ExtraProperties { get; protected set; }

        IEnumerable<IProductAttributeOption> IProductAttribute.ProductAttributeOptions => ProductAttributeOptions;
        public virtual List<ProductAttributeOption> ProductAttributeOptions { get; protected set; }

        protected ProductAttribute()
        {
            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
        }

        public ProductAttribute(
            Guid id,
            [NotNull] string displayName,
            [CanBeNull] string description,
            int displayOrder = 0) : base(id)
        {
            DisplayName = displayName;
            Description = description;
            DisplayOrder = displayOrder;

            ProductAttributeOptions = new List<ProductAttributeOption>();

            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
        }
    }
}