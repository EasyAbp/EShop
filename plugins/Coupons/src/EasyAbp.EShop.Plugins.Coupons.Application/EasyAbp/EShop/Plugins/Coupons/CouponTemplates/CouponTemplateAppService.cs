using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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

        public override async Task<CouponTemplateDto> CreateAsync(CreateUpdateCouponTemplateDto input)
        {
            await CheckCreatePolicyAsync();

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
            await CheckUpdatePolicyAsync();

            var entity = await GetEntityByIdAsync(id);
            
            await MapToEntityAsync(input, entity);

            foreach (var inputScoreDto in input.Scopes.Where(dto => !ExistScope(entity.Scopes, dto)))
            {
                entity.Scopes.Add(new CouponTemplateScope(GuidGenerator.Create(), inputScoreDto.StoreId,
                    inputScoreDto.ProductGroupName, inputScoreDto.ProductId, inputScoreDto.ProductSkuId));
            }
            
            foreach (var scope in entity.Scopes.Where(scope => !ExistScope(input.Scopes, scope)))
            {
                entity.Scopes.Remove(scope);
            }
            
            await Repository.UpdateAsync(entity, autoSave: true);

            return await MapToGetOutputDtoAsync(entity);
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
