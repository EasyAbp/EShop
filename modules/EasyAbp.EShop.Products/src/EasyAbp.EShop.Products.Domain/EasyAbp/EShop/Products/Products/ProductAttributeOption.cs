using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Data;
using Volo.Abp.Domain.Entities.Auditing;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductAttributeOption : FullAuditedEntity<Guid>, IProductAttributeOption
    {
        [NotNull]
        public virtual string DisplayName { get; protected set; }
        
        [CanBeNull]
        public virtual string Description { get; protected set; }
        
        public virtual int DisplayOrder { get; protected set; }

        public virtual Dictionary<string, object> ExtraProperties { get; protected set; }

        protected ProductAttributeOption()
        {
            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();
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
            
            ExtraProperties = new Dictionary<string, object>();
            this.SetDefaultsForExtraProperties();
        }
    }
}