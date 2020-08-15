using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Permissions;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

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
        : ApplicationService
            , IReadOnlyAppService<TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
        where TEntity : class, IEntity, IMultiStore
    {
        protected IReadOnlyRepository<TEntity> ReadOnlyRepository { get; }

        protected virtual string GetPolicyName { get; set; }

        protected virtual string GetListPolicyName { get; set; }

        protected virtual string CrossStorePolicyName { get; set; }

        protected MultiStoreAbstractKeyReadOnlyAppService(IReadOnlyRepository<TEntity> repository)
        {
            ReadOnlyRepository = repository;
        }

        public virtual async Task<TGetOutputDto> GetAsync(TKey id)
        {
            var entity = await GetEntityByIdAsync(id);

            await CheckMultiStorePolicyAsync(entity.StoreId, GetPolicyName);

            return await MapToGetOutputDtoAsync(entity);
        }

        public virtual async Task<PagedResultDto<TGetListOutputDto>> GetListAsync(TGetListInput input)
        {
            await CheckGetListPolicyAsync();

            var query = CreateFilteredQuery(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<TGetListOutputDto>(
                totalCount,
                entityDtos
            );
        }
        
        protected virtual async Task CheckGetListPolicyAsync()
        {
            await CheckPolicyAsync(GetListPolicyName);
        }

        protected abstract Task<TEntity> GetEntityByIdAsync(TKey id);

        protected virtual async Task CheckMultiStorePolicyAsync(Guid storeId, string policyName)
        {
            if (!await AuthorizationService.IsStoreOwnerGrantedAsync(storeId, policyName))
            {
                await CheckPolicyAsync(CrossStorePolicyName);
                await CheckPolicyAsync(policyName);
            }
        }

        /// <summary>
        /// Should apply sorting if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, TGetListInput input)
        {
            //Try to sort query if available
            if (input is ISortedResultRequest sortInput)
            {
                if (!sortInput.Sorting.IsNullOrWhiteSpace())
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }

            //IQueryable.Task requires sorting, so we should sort if Take will be used.
            if (input is ILimitedResultRequest)
            {
                return ApplyDefaultSorting(query);
            }

            //No sorting
            return query;
        }

        /// <summary>
        /// Applies sorting if no sorting specified but a limited result requested.
        /// </summary>
        /// <param name="query">The query.</param>
        protected virtual IQueryable<TEntity> ApplyDefaultSorting(IQueryable<TEntity> query)
        {
            if (typeof(TEntity).IsAssignableTo<IHasCreationTime>())
            {
                return query.OrderByDescending(e => ((IHasCreationTime) e).CreationTime);
            }

            throw new AbpException(
                "No sorting specified but this query requires sorting. Override the ApplyDefaultSorting method for your application service derived from AbstractKeyReadOnlyAppService!");
        }

        /// <summary>
        /// Should apply paging if needed.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> ApplyPaging(IQueryable<TEntity> query, TGetListInput input)
        {
            //Try to use paging if available
            if (input is IPagedResultRequest pagedInput)
            {
                return query.PageBy(pagedInput);
            }

            //Try to limit query result if available
            if (input is ILimitedResultRequest limitedInput)
            {
                return query.Take(limitedInput.MaxResultCount);
            }

            //No paging
            return query;
        }

        /// <summary>
        /// This method should create <see cref="IQueryable{TEntity}"/> based on given input.
        /// It should filter query if needed, but should not do sorting or paging.
        /// Sorting should be done in <see cref="ApplySorting"/> and paging should be done in <see cref="ApplyPaging"/>
        /// methods.
        /// </summary>
        /// <param name="input">The input.</param>
        protected virtual IQueryable<TEntity> CreateFilteredQuery(TGetListInput input)
        {
            return ReadOnlyRepository;
        }

        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TGetOutputDto"/>.
        /// It internally calls the <see cref="MapToGetOutputDto"/> by default.
        /// It can be overriden for custom mapping.
        /// Overriding this has higher priority than overriding the <see cref="MapToGetOutputDto"/>
        /// </summary>
        protected virtual Task<TGetOutputDto> MapToGetOutputDtoAsync(TEntity entity)
        {
            return Task.FromResult(MapToGetOutputDto(entity));
        }

        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TGetOutputDto"/>.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overriden for custom mapping.
        /// </summary>
        protected virtual TGetOutputDto MapToGetOutputDto(TEntity entity)
        {
            return ObjectMapper.Map<TEntity, TGetOutputDto>(entity);
        }

        /// <summary>
        /// Maps a list of <see cref="TEntity"/> to <see cref="TGetListOutputDto"/> objects.
        /// It uses <see cref="MapToGetListOutputDtoAsync"/> method for each item in the list.
        /// </summary>
        protected virtual async Task<List<TGetListOutputDto>> MapToGetListOutputDtosAsync(List<TEntity> entities)
        {
            var dtos = new List<TGetListOutputDto>();

            foreach (var entity in entities)
            {
                dtos.Add(await MapToGetListOutputDtoAsync(entity));
            }

            return dtos;
        }

        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TGetListOutputDto"/>.
        /// It internally calls the <see cref="MapToGetListOutputDto"/> by default.
        /// It can be overriden for custom mapping.
        /// Overriding this has higher priority than overriding the <see cref="MapToGetListOutputDto"/>
        /// </summary>
        protected virtual Task<TGetListOutputDto> MapToGetListOutputDtoAsync(TEntity entity)
        {
            return Task.FromResult(MapToGetListOutputDto(entity));
        }

        /// <summary>
        /// Maps <see cref="TEntity"/> to <see cref="TGetListOutputDto"/>.
        /// It uses <see cref="IObjectMapper"/> by default.
        /// It can be overriden for custom mapping.
        /// </summary>
        protected virtual TGetListOutputDto MapToGetListOutputDto(TEntity entity)
        {
            return ObjectMapper.Map<TEntity, TGetListOutputDto>(entity);
        }
    }
}