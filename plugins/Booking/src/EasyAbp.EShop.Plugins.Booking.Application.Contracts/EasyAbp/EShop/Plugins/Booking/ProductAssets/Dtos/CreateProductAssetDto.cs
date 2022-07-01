using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Stores.Stores;

namespace EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos
{
    [Serializable]
    public class CreateProductAssetDto : IMultiStore, IValidatableObject
    {
        public Guid StoreId { get; set; }

        public Guid ProductId { get; set; }

        public Guid ProductSkuId { get; set; }

        public Guid AssetId { get; set; }

        public Guid PeriodSchemeId { get; set; }

        public DateTime FromTime { get; set; }

        public DateTime? ToTime { get; set; }

        public string Currency { get; set; }

        [Range(BookingConsts.MinimumPrice, BookingConsts.MaximumPrice)]
        public decimal? Price { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Price is not null && Currency.IsNullOrWhiteSpace())
            {
                yield return new ValidationResult(
                    "Currency should not be empty when the Price has a value!",
                    new[] { nameof(Currency) }
                );
            }
        }
    }
}