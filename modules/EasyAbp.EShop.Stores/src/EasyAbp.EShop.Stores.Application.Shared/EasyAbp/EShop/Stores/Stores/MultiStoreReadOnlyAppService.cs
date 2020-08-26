using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Stores.Stores
{
    public abstract class MultiStoreReadOnlyAppService<TEntity, TEntityDto, TKey>
        : MultiStoreReadOnlyAppService<TEntity, TEntityDto, TEntityDto, TKey, PagedAndSortedResultRequestDto>
        where TEntity : class, IEntity<TKey>, IMultiStore
        where TEntityDto : IEntityDto<TKey>
    {
        protected MultiStoreReadOnlyAppService(IReadOnlyRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreReadOnlyAppService<TEntity, TEntityDto, TKey, TGetListInput>
        : MultiStoreReadOnlyAppService<TEntity, TEntityDto, TEntityDto, TKey, TGetListInput>
        where TEntity : class, IEntity<TKey>, IMultiStore
        where TEntityDto : IEntityDto<TKey>
    {
        protected MultiStoreReadOnlyAppService(IReadOnlyRepository<TEntity, TKey> repository)
            : base(repository)
        {
        }
    }

    public abstract class MultiStoreReadOnlyAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
        : MultiStoreAbstractKeyReadOnlyAppService<TEntity, TGetOutputDto, TGetListOutputDto, TKey, TGetListInput>
        where TEntity : class, IEntity<TKey>, IMultiStore
        where TGetOutputDto : IEntityDto<TKey>
        where TGetListOutputDto : IEntityDto<TKey>
    {
        protected IReadOnlyRepository<TEntity, TKey> Repository { get; }

        protected MultiStoreReadOnlyAppService(IReadOnlyRepository<TEntity, TKey> repository)
            : base(repository)
        {
            Repository = repository;
        }

        protected override async Task<TEntity> GetEntityByIdAsync(TKey id)
        {
            return await Repository.GetAsync(id);
        }

        protected override IQueryable<TEntity> ApplyDefaultSorting(IQueryable<TEntity> query)
        {
            if (typeof(TEntity).IsAssignableTo<ICreationAuditedObject>())
            {
                return query.OrderByDescending(e => ((ICreationAuditedObject)e).CreationTime);
            }
            else
            {
                return query.OrderByDescending(e => e.Id);
            }
        }
    }
}