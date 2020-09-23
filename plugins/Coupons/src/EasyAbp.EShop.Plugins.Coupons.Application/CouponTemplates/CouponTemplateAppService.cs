using System;
using EasyAbp.EShop.Plugins.Coupons.Permissions;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public class CouponTemplateAppService : CrudAppService<CouponTemplate, CouponTemplateDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCouponTemplateDto, CreateUpdateCouponTemplateDto>,
        ICouponTemplateAppService
    {
        protected override string GetPolicyName { get; set; } = CouponsPermissions.CouponTemplate.Default;
        protected override string GetListPolicyName { get; set; } = CouponsPermissions.CouponTemplate.Default;
        protected override string CreatePolicyName { get; set; } = CouponsPermissions.CouponTemplate.Create;
        protected override string UpdatePolicyName { get; set; } = CouponsPermissions.CouponTemplate.Update;
        protected override string DeletePolicyName { get; set; } = CouponsPermissions.CouponTemplate.Delete;

        private readonly ICouponTemplateRepository _repository;
        
        public CouponTemplateAppService(ICouponTemplateRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
