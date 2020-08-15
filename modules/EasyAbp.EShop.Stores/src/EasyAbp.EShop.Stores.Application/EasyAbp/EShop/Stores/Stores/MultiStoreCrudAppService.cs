using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Stores.Stores
{
    public abstract class MultiStoreCrudAppService<TEntity, TEntityDto, TKey>
        : MultiStoreCrudAppService<TEntity, TEntityDto, TKey, PagedAndSortedResultRequestDto>
        where TEntity : class, IEntity<TKey>, IMultiStore
        where TEntityDto : IEntityDto<TKey>, IMultiStore
    {
        protected MultiStoreCrudAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreCrudAppService<TEntity, TEntityDto, TKey, TGetListInput>
        : MultiStoreCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TEntityDto, TEntityDto>
        where TEntity : class, IEntity<TKey>, IMultiStore
        where TEntityDto : IEntityDto<TKey>, IMultiStore
    {
        protected MultiStoreCrudAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput>
        : MultiStoreCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TCreateInput>
        where TEntity : class, IEntity<TKey>, IMultiStore
        where TEntityDto : IEntityDto<TKey>
        where TCreateInput : IMultiStore
    {
        protected MultiStoreCrudAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreCrudAppService<TEntity, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        : MultiStoreCrudAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput, TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TKey>, IMultiStore
        where TEntityDto : IEntityDto<TKey>
        where TCreateInput : IMultiStore
    {
        protected MultiStoreCrudAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }

        protected override Task<TEntityDto> MapToGetListOutputDtoAsync(TEntity entity)
        {
            return base.MapToGetOutputDtoAsync(entity);
        }

        protected override TEntityDto MapToGetListOutputDto(TEntity entity)
        {
            return MapToGetOutputDto(entity);
        }
    }

    public abstract class MultiStoreCrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput,
            TCreateInput, TUpdateInput>
        : MultiStoreAbstractKeyCrudAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput,
            TCreateInput, TUpdateInput>
        where TEntity : class, IEntity<TKey>, IMultiStore
        where TGetOutputDto : IEntityDto<TKey>
        where TGetListOutputDto : IEntityDto<TKey>
        where TCreateInput : IMultiStore
    {
        protected new IRepository<TEntity, TKey> Repository { get; }

        protected MultiStoreCrudAppService(IRepository<TEntity, TKey> repository)
            : base(repository)
        {
            Repository = repository;
        }

        protected override async Task DeleteByIdAsync(TKey id)
        {
            await Repository.DeleteAsync(id);
        }

        protected override async Task<TEntity> GetEntityByIdAsync(TKey id)
        {
            return await Repository.GetAsync(id);
        }

        protected override void MapToEntity(TUpdateInput updateInput, TEntity entity)
        {
            if (updateInput is IEntityDto<TKey> entityDto)
            {
                entityDto.Id = entity.Id;
            }

            base.MapToEntity(updateInput, entity);
        }

        protected override IQueryable<TEntity> ApplyDefaultSorting(IQueryable<TEntity> query)
        {
            if (typeof(TEntity).IsAssignableTo<IHasCreationTime>())
            {
                return query.OrderByDescending(e => ((IHasCreationTime) e).CreationTime);
            }
            else
            {
                return query.OrderByDescending(e => e.Id);
            }
        }
    }
}