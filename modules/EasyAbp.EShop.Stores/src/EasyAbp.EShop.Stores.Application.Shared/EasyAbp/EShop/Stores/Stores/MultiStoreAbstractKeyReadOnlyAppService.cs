using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Stores.Stores
{
    public abstract class MultiStoreAbstractKeyReadOnlyAppService<TEntity, TEntityDto, TKey>
        : MultiStoreAbstractKeyReadOnlyAppService<TEntity, TEntityDto, TEntityDto, TKey, PagedAndSortedResultRequestDto>
        where TEntity : class, IEntity, IMultiStore
    {
        protected MultiStoreAbstractKeyReadOnlyAppService(IReadOnlyRepository<TEntity> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreAbstractKeyReadOnlyAppService<TEntity, TEntityDto, TKey, TGetListInput>
        : MultiStoreAbstractKeyReadOnlyAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput>
        where TEntity : class, IEntity, IMultiStore
    {
        protected MultiStoreAbstractKeyReadOnlyAppService(IReadOnlyRepository<TEntity> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreAbstractKeyReadOnlyAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey,
            TGetListInput>
        : AbstractKeyReadOnlyAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey,
                TGetListInput>
        where TEntity : class, IEntity, IMultiStore
    {
        protected virtual string CrossStorePolicyName { get; set; }

        protected MultiStoreAbstractKeyReadOnlyAppService(IReadOnlyRepository<TEntity> repository)
            : base(repository)
        {
        }

        public override async Task<TGetOutputDto> GetAsync(TKey id)
        {
            var entity = await GetEntityByIdAsync(id);

            await CheckMultiStorePolicyAsync(entity.StoreId, GetPolicyName);

            return MapToGetOutputDto(entity);
        }

        protected virtual async Task CheckMultiStorePolicyAsync(Guid? storeId, string policyName, bool crossStoreAllowed = true)
        {
            if (crossStoreAllowed)
            {
                await AuthorizationService.CheckMultiStorePolicyAsync(storeId, policyName, CrossStorePolicyName);
            }
            else
            {
                if (!storeId.HasValue)
                {
                    throw new AbpAuthorizationException("Authorization failed! StoreId can not be null!");
                }
                
                await AuthorizationService.CheckMultiStorePolicyAsync(storeId.Value, policyName);
            }
        }
    }
}