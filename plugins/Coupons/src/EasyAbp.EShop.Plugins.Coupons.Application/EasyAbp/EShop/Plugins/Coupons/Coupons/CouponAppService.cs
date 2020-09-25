using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.Permissions;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class CouponAppService : CrudAppService<Coupon, CouponDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCouponDto, CreateUpdateCouponDto>,
        ICouponAppService
    {
        protected override string GetPolicyName { get; set; } = CouponsPermissions.Coupon.Default;
        protected override string GetListPolicyName { get; set; } = CouponsPermissions.Coupon.Default;
        protected override string CreatePolicyName { get; set; } = CouponsPermissions.Coupon.Create;
        protected override string UpdatePolicyName { get; set; } = CouponsPermissions.Coupon.Update;
        protected override string DeletePolicyName { get; set; } = CouponsPermissions.Coupon.Delete;

        private readonly ICouponTemplateRepository _couponTemplateRepository;
        private readonly ICouponRepository _repository;
        
        public CouponAppService(
            ICouponTemplateRepository couponTemplateRepository,
            ICouponRepository repository) : base(repository)
        {
            _couponTemplateRepository = couponTemplateRepository;
            _repository = repository;
        }

        public override async Task<CouponDto> CreateAsync(CreateUpdateCouponDto input)
        {
            await CheckCreatePolicyAsync();

            var couponTemplate = await _couponTemplateRepository.GetAsync(input.CouponTemplateId);
            
            var entity = await MapToEntityAsync(input);

            TryToSetTenantId(entity);
            
            entity.SetExpirationTime(couponTemplate.GetCalculatedExpirationTime(Clock));

            await Repository.InsertAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }
    }
}
