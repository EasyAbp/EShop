using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications.Dtos
{
    [Serializable]
    public class CreateStoreApplicationDto : UpdateStoreApplicationDto
    {
        [Required]
        public Guid ApplicantId { get; set; }
    }

    [Serializable]
    public class UpdateStoreApplicationDto
    {
        [Required]
        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxNameLength))]
        public string StoreName { get; set; }

        [Required]
        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxNameLength))]
        public string BusinessCategory { get; set; }

        [Required]
        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxAddressLength))]
        public string Address { get; set; }

        [Required]
        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxNameLength))]
        public string UnifiedCreditCode { get; set; }

        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxNameLength))]
        public string HouseNumber { get; set; }

        [Required]
        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxImageResourcesLength))]
        public string BusinessLicenseImage { get; set; }

        [Required]
        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxNameLength))]
        public string Name { get; set; }

        [Required]
        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxNameLength))]
        public string IdNumber { get; set; }

        [Required]
        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxImageResourcesLength))]
        public string IdCardFrontImage { get; set; }

        [Required]
        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxImageResourcesLength))]
        public string IdCardBackImage { get; set; }

        [Required]
        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxImageResourcesLength))]
        public string StoreImage { get; set; }

        [DynamicMaxLength(typeof(StoreApplicationConsts), nameof(StoreApplicationConsts.MaxNoteLength))]
        public string Note { get; set; }
    }
    
}