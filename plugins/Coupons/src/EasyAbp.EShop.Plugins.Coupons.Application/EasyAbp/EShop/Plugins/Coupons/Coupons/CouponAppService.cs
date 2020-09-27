using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.Permissions;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class CouponAppService : CrudAppService<Coupon, CouponDto, Guid, GetCouponListInput, CreateCouponDto, UpdateCouponDto>,
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

        protected override IQueryable<Coupon> CreateFilteredQuery(GetCouponListInput input)
        {
            return input.AvailableOnly ? _repository.GetAvailableCouponQueryable(Clock) : _repository.AsQueryable()
                .WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId.Value);
        }

        public override async Task<CouponDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var entity = await GetEntityByIdAsync(id);

            if (entity.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(CouponsPermissions.Coupon.Manage);
            }

            return await MapToGetOutputDtoAsync(entity);
        }

        public override async Task<PagedResultDto<CouponDto>> GetListAsync(GetCouponListInput input)
        {
            if (input.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(CouponsPermissions.Coupon.Manage);
            }
            
            return await base.GetListAsync(input);
        }

        public override async Task<CouponDto> CreateAsync(CreateCouponDto input)
        {
            await CheckCreatePolicyAsync();

            var couponTemplate = await _couponTemplateRepository.GetAsync(input.CouponTemplateId);
            
            var entity = await MapToEntityAsync(input);

            TryToSetTenantId(entity);
            
            entity.SetExpirationTime(couponTemplate.GetCalculatedExpirationTime(Clock));

            await Repository.InsertAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }

        [Authorize(CouponsPermissions.Coupon.Use)]
        public virtual async Task<CouponDto> OccupyAsync(Guid id, OccupyCouponInput input)
        {
            var coupon = await GetEntityByIdAsync(id);

            if (coupon.OrderId.HasValue)
            {
                throw new CouponHasBeenOccupiedException();
            }
            
            coupon.SetOrderId(input.OrderId);

            await _repository.UpdateAsync(coupon, true);

            return ObjectMapper.Map<Coupon, CouponDto>(coupon);
        }
    }
}
