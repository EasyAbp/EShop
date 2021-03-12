using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Coupons.Permissions;
using EasyAbp.EShop.Plugins.Coupons.CouponTemplates.Dtos;
using EasyAbp.EShop.Stores.Authorization;
using Microsoft.AspNetCore.Authorization;
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

        protected override async Task<IQueryable<CouponTemplate>> CreateFilteredQueryAsync(PagedAndSortedResultRequestDto input)
        {
            return (await _repository.WithDetailsAsync());
        }

        public override async Task<CouponTemplateDto> CreateAsync(CreateUpdateCouponTemplateDto input)
        {
            await AuthorizationService.CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName,
                CouponsPermissions.CouponTemplate.CrossStore);
            
            if (input.Scopes.Any(x => x.StoreId != input.StoreId))
            {
                await AuthorizationService.CheckAsync(CouponsPermissions.CouponTemplate.CrossStore);
            }
            
            var entity = await MapToEntityAsync(input);

            TryToSetTenantId(entity);

            foreach (var inputScoreDto in input.Scopes)
            {
                entity.Scopes.Add(new CouponTemplateScope(GuidGenerator.Create(), inputScoreDto.StoreId,
                    inputScoreDto.ProductGroupName, inputScoreDto.ProductId, inputScoreDto.ProductSkuId));
            }

            await Repository.InsertAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }

        public override async Task<CouponTemplateDto> UpdateAsync(Guid id, CreateUpdateCouponTemplateDto input)
        {
            var entity = await GetEntityByIdAsync(id);
            
            await AuthorizationService.CheckMultiStorePolicyAsync(entity.StoreId, UpdatePolicyName,
                CouponsPermissions.CouponTemplate.CrossStore);

            if (input.StoreId != entity.StoreId || input.Scopes.Any(x => x.StoreId != input.StoreId))
            {
                await AuthorizationService.CheckAsync(CouponsPermissions.CouponTemplate.CrossStore);
            }
            
            await MapToEntityAsync(input, entity);

            foreach (var inputScoreDto in input.Scopes.Where(dto => !ExistScope(entity.Scopes, dto)))
            {
                entity.Scopes.Add(new CouponTemplateScope(GuidGenerator.Create(), inputScoreDto.StoreId,
                    inputScoreDto.ProductGroupName, inputScoreDto.ProductId, inputScoreDto.ProductSkuId));
            }
            
            entity.Scopes.RemoveAll(scope => !ExistScope(input.Scopes, scope));

            await Repository.UpdateAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
        }

        public override async Task DeleteAsync(Guid id)
        {
            var couponTemplate = await GetEntityByIdAsync(id);
            
            await AuthorizationService.CheckMultiStorePolicyAsync(couponTemplate.StoreId, DeletePolicyName,
                CouponsPermissions.CouponTemplate.CrossStore);

            await _repository.DeleteAsync(couponTemplate);
        }

        protected virtual bool ExistScope(IEnumerable<CreateUpdateCouponTemplateScopeDto> scopes, ICouponTemplateScope need)
        {
            return scopes.Any(x => x.StoreId == need.StoreId &&
                                   x.ProductGroupName == need.ProductGroupName &&
                                   x.ProductId == need.ProductId &&
                                   x.ProductSkuId == need.ProductSkuId);
        }
        
        protected virtual bool ExistScope(IEnumerable<ICouponTemplateScope> scopes, CreateUpdateCouponTemplateScopeDto dto)
        {
            return scopes.Any(x => x.StoreId == dto.StoreId &&
                                   x.ProductGroupName == dto.ProductGroupName &&
                                   x.ProductId == dto.ProductId &&
                                   x.ProductSkuId == dto.ProductSkuId);
        }
    }
}
