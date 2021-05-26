using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.Permissions;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using EasyAbp.EShop.Stores.Authorization;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
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

        protected override async Task<IQueryable<Coupon>> CreateFilteredQueryAsync(GetCouponListInput input)
        {
            return (input.AvailableOnly ? _repository.GetAvailableCouponQueryable(Clock) : _repository.AsQueryable())
                .WhereIf(input.UserId.HasValue, x => x.UserId == input.UserId.Value)
                .WhereIf(!input.AvailableOnly && !input.IncludesUsed, x => !x.UsedTime.HasValue)
                .WhereIf(!input.AvailableOnly && !input.IncludesExpired, x => !x.ExpirationTime.HasValue || x.ExpirationTime.Value > Clock.Now);
        }

        protected virtual CouponDto FillCouponTemplateData(CouponDto couponDto, CouponTemplate couponTemplate)
        {
            couponDto.CouponTemplate = ObjectMapper.Map<CouponTemplate, CouponTemplateDto>(couponTemplate);

            return couponDto;
        }

        public override async Task<CouponDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var coupon = await GetEntityByIdAsync(id);

            var couponTemplate = await _couponTemplateRepository.GetAsync(coupon.CouponTemplateId);

            if (coupon.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckMultiStorePolicyAsync(couponTemplate.StoreId,
                    CouponsPermissions.Coupon.Manage, CouponsPermissions.Coupon.CrossStore);
            }

            return FillCouponTemplateData(await MapToGetOutputDtoAsync(coupon), couponTemplate);
        }

        [Authorize]
        public override async Task<PagedResultDto<CouponDto>> GetListAsync(GetCouponListInput input)
        {
            if (input.UserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId,
                    CouponsPermissions.Coupon.Manage, CouponsPermissions.Coupon.CrossStore);
            }
            
            var result = await base.GetListAsync(input);

            var couponTemplateDtoDictionary = new Dictionary<Guid, CouponTemplateDto>();

            foreach (var couponDto in result.Items)
            {
                if (!couponTemplateDtoDictionary.ContainsKey(couponDto.CouponTemplateId))
                {
                    couponTemplateDtoDictionary.Add(couponDto.CouponTemplateId,
                        ObjectMapper.Map<CouponTemplate, CouponTemplateDto>(
                            await _couponTemplateRepository.GetAsync(couponDto.CouponTemplateId)));
                }

                couponDto.CouponTemplate = couponTemplateDtoDictionary[couponDto.CouponTemplateId];
            }

            return result;
        }

        public override async Task<CouponDto> CreateAsync(CreateCouponDto input)
        {
            await CheckCreatePolicyAsync();

            var couponTemplate = await _couponTemplateRepository.GetAsync(input.CouponTemplateId);
            
            await AuthorizationService.CheckMultiStorePolicyAsync(couponTemplate.StoreId,
                CouponsPermissions.Coupon.Manage, CouponsPermissions.Coupon.CrossStore);
            
            var coupon = await MapToEntityAsync(input);

            TryToSetTenantId(coupon);
            
            coupon.SetExpirationTime(couponTemplate.GetCalculatedExpirationTime(Clock));

            await _repository.InsertAsync(coupon, autoSave: true);

            return FillCouponTemplateData(await MapToGetOutputDtoAsync(coupon), couponTemplate);
        }

        public override async Task<CouponDto> UpdateAsync(Guid id, UpdateCouponDto input)
        {
            await CheckUpdatePolicyAsync();

            var coupon = await GetEntityByIdAsync(id);
            
            var couponTemplate = await _couponTemplateRepository.GetAsync(coupon.CouponTemplateId);

            await AuthorizationService.CheckMultiStorePolicyAsync(couponTemplate.StoreId,
                CouponsPermissions.Coupon.Manage, CouponsPermissions.Coupon.CrossStore);
            
            await MapToEntityAsync(input, coupon);
            
            await Repository.UpdateAsync(coupon, autoSave: true);
            
            return FillCouponTemplateData(await MapToGetOutputDtoAsync(coupon), couponTemplate);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await CheckDeletePolicyAsync();
            
            var coupon = await GetEntityByIdAsync(id);
            
            var couponTemplate = await _couponTemplateRepository.GetAsync(coupon.CouponTemplateId);

            await AuthorizationService.CheckMultiStorePolicyAsync(couponTemplate.StoreId,
                CouponsPermissions.Coupon.Manage, CouponsPermissions.Coupon.CrossStore);

            await _repository.DeleteAsync(coupon, true);
        }

        [Authorize(CouponsPermissions.Coupon.Use)]
        public virtual async Task<CouponDto> OccupyAsync(Guid id, OccupyCouponInput input)
        {
            var coupon = await GetEntityByIdAsync(id);

            if (coupon.UserId != CurrentUser.GetId())
            {
                throw new AbpAuthorizationException();
            }

            if (coupon.OrderId.HasValue)
            {
                throw new CouponHasBeenOccupiedException();
            }
            
            var couponTemplate = await _couponTemplateRepository.GetAsync(coupon.CouponTemplateId);

            coupon.SetOrderId(input.OrderId);

            await _repository.UpdateAsync(coupon, true);

            return FillCouponTemplateData(await MapToGetOutputDtoAsync(coupon), couponTemplate);
        }
    }
}
