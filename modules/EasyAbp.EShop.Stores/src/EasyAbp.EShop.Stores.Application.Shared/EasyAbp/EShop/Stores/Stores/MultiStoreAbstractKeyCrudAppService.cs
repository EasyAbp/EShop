using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Permissions;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Stores.Stores
{
    public abstract class MultiStoreAbstractKeyCrudAppService<TEntity, TEntityDto, TKey>
        : MultiStoreAbstractKeyCrudAppService<TEntity, TEntityDto, TKey, PagedAndSortedResultRequestDto>
        where TEntity : class, IEntity, IMultiStore
        where TEntityDto : IMultiStore
    {
        protected MultiStoreAbstractKeyCrudAppService(IRepository<TEntity> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreAbstractKeyCrudAppService<TEntity, TEntityDto, TKey, TGetListInput>
        : MultiStoreAbstractKeyCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto, TEntityDto>
        where TEntity : class, IEntity, IMultiStore
        where TEntityDto : IMultiStore
    {
        protected MultiStoreAbstractKeyCrudAppService(IRepository<TEntity> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreAbstractKeyCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        : MultiStoreAbstractKeyCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
        where TEntity : class, IEntity, IMultiStore
        where TCreateInput : IMultiStore
    {
        protected MultiStoreAbstractKeyCrudAppService(IRepository<TEntity> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreAbstractKeyCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput,
            TUpdateInput>
        : MultiStoreAbstractKeyCrudAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput,
            TUpdateInput>
        where TEntity : class, IEntity, IMultiStore
        where TCreateInput : IMultiStore
    {
        protected MultiStoreAbstractKeyCrudAppService(IRepository<TEntity> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreAbstractKeyCrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey,
            TGetListInput, TCreateInput, TUpdateInput>
        : AbstractKeyCrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey,
                TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity, IMultiStore
        where TCreateInput : IMultiStore
    {        
        protected virtual string CrossStorePolicyName { get; set; }
        
        protected MultiStoreAbstractKeyCrudAppService(IRepository<TEntity> repository)
            : base(repository)
        {
        }
        
        public override async Task<TGetOutputDto> GetAsync(TKey id)
        {
            var entity = await GetEntityByIdAsync(id);

            await CheckMultiStorePolicyAsync(entity.StoreId, GetPolicyName);

            return MapToGetOutputDto(entity);
        }
        
        public override async Task<TGetOutputDto> CreateAsync(TCreateInput input)
        {
            await CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName);

            var entity = MapToEntity(input);

            TryToSetTenantId(entity);

            await Repository.InsertAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public override async Task<TGetOutputDto> UpdateAsync(TKey id, TUpdateInput input)
        {
            var entity = await GetEntityByIdAsync(id);
            await CheckMultiStorePolicyAsync(entity.StoreId, UpdatePolicyName);
            
            MapToEntity(input, entity);
            await Repository.UpdateAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public override async Task DeleteAsync(TKey id)
        {
            var entity = await GetEntityByIdAsync(id);
            await CheckMultiStorePolicyAsync(entity.StoreId, DeletePolicyName);

            await DeleteByIdAsync(id);
        }


        protected virtual async Task CheckMultiStorePolicyAsync(Guid? storeId, string policyName)
        {
            if (storeId.HasValue
                && await AuthorizationService.IsStoreOwnerGrantedAsync(storeId.Value, policyName))
            {
                return;
            }

            await CheckPolicyAsync(CrossStorePolicyName);
            await CheckPolicyAsync(policyName);
        }
    }
}