using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos
{
    [Serializable]
    public class CreateUpdateCouponTemplateDto : IValidatableObject
    {
        public Guid? StoreId { get; set; }

        public CouponType CouponType { get; set; }

        public string UniqueName { get; set; }

        public string DisplayName { get; set; }

        public string Description { get; set; }

        public TimeSpan? UsableDuration { get; set; }

        public DateTime? UsableBeginTime { get; set; }

        public DateTime? UsableEndTime { get; set; }

        public decimal ConditionAmount { get; set; }

        public decimal DiscountAmount { get; set; }

        public bool IsUnscoped { get; set; }

        public List<CreateUpdateCouponTemplateScopeDto> Scopes { get; set; } = new List<CreateUpdateCouponTemplateScopeDto>();
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (DiscountAmount > ConditionAmount)
            {
                yield return new ValidationResult(
                    "DiscountAmount should not be greater than ConditionAmount!",
                    new[] { nameof(DiscountAmount) }
                );
            }
            
            if (CouponType == CouponType.PerMeet && ConditionAmount == decimal.Zero)
            {
                yield return new ValidationResult(
                    "ConditionAmount should be greater than zero if the CouponType is PerMeet!",
                    new[] { nameof(ConditionAmount) }
                );
            }
        }
    }
}