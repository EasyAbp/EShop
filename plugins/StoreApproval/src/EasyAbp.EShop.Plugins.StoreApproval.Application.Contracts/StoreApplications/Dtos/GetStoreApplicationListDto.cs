using System;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications.Dtos
{
    public class GetStoreApplicationListDto : PagedAndSortedResultRequestDto
    {
        public Guid? ApplicantId { get; set; }

        public string StoreName { get; set; }

        public string BusinessCategory { get; protected set; }

        public string UnifiedCreditCode { get; protected set; }

        public string Name { get; protected set; }

        public string IdNumber { get; protected set; }
    }
}
