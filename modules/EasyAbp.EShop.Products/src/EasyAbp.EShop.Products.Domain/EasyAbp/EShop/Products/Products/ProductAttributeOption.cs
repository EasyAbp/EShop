using System;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.DynamicProxy;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttributeOption : FullAuditedEntity<Guid>, IProductAttributeOption
    {
        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        [CanBeNull]
        public virtual string Description { get; protected set; }
        
        public virtual int DisplayOrder { get; protected set; }

        public ExtraPropertyDictionary ExtraProperties { get; protected set; }

        protected ProductAttributeOption()
        {
            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
        }
        
        public ProductAttributeOption(
            Guid id,
            [NotNull] string displayName,
            [CanBeNull] string description,
            int displayOrder = 0) : base(id)
        {
            DisplayName = displayName;
            Description = description;
            DisplayOrder = displayOrder;
            
            ExtraProperties = new ExtraPropertyDictionary();
            this.SetDefaultsForExtraProperties(ProxyHelper.UnProxy(this).GetType());
        }
    }
}