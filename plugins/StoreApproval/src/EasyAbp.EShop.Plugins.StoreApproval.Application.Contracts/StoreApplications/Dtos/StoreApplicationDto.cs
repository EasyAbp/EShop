using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications.Dtos
{
    [Serializable]
    public class StoreApplicationDto : FullAuditedEntityDto<Guid>
    {
        public Guid ApplicantId { get; set; }

        public ApprovalStatusType ApprovalStatus { get; set; }

        public string StoreName { get; set; }

        public string BusinessCategory { get; set; }

        public string Address { get; set; }

        public string UnifiedCreditCode { get; set; }

        public string HouseNumber { get; set; }

        public string BusinessLicenseImage { get; set; }

        public string Name { get; set; }

        public string IdNumber { get; set; }

        public string IdCardFrontImage { get; set; }

        public string IdCardBackImage { get; set; }

        public string StoreImage { get; set; }

        public string Note { get; set; }
    }
}