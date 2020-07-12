using EasyAbp.EShop.Products.Permissions;
using EasyAbp.EShop.Products.Tags.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Linq;

namespace EasyAbp.EShop.Products.Tags
{
    public class TagAppService : CrudAppService<Tag, TagDto, Guid, GetTagListDto, CreateTagDto, UpdateTagDto>,
        ITagAppService
    {
        private readonly IAsyncQueryableExecuter _asyncQueryableExecuter;
        protected override string CreatePolicyName { get; set; } = ProductsPermissions.Tags.Create;
        protected override string DeletePolicyName { get; set; } = ProductsPermissions.Tags.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductsPermissions.Tags.Update;
        protected override string GetPolicyName { get; set; } = ProductsPermissions.Tags.Default;
        protected override string GetListPolicyName { get; set; } = ProductsPermissions.Tags.Default;

        public TagAppService(ITagRepository repository,
            [NotNull] IAsyncQueryableExecuter asyncQueryableExecuter) : base(repository)
        {
            _asyncQueryableExecuter = asyncQueryableExecuter ?? throw new ArgumentNullException(nameof(asyncQueryableExecuter));
        }

        protected override IQueryable<Tag> CreateFilteredQuery(GetTagListDto input)
        {
            var query = base.CreateFilteredQuery(input);

            query = query.Where(x => x.StoreId == input.StoreId);

            return input.ShowHidden ? query : query.Where(x => !x.IsHidden);
        }

        public override async Task<TagDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var entity = await GetEntityByIdAsync(id);

            await CheckCurrentUserIsStoreOwner(entity.StoreId, CurrentUser.Id);

            return MapToGetOutputDto(entity);
        }

        public override async Task<PagedResultDto<TagDto>> GetListAsync(GetTagListDto input)
        {
            await CheckGetListPolicyAsync();

            await CheckCurrentUserIsStoreOwner(input.StoreId, CurrentUser.Id);

            var query = CreateFilteredQuery(input);

            var totalCount = await _asyncQueryableExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await _asyncQueryableExecuter.ToListAsync(query);

            return new PagedResultDto<TagDto>(
                totalCount,
                entities.Select(MapToGetListOutputDto).ToList());
        }

        public override async Task<TagDto> CreateAsync(CreateTagDto input)
        {
            await CheckCreatePolicyAsync();

            await CheckCurrentUserIsStoreOwner(input.StoreId, CurrentUser.Id);

            var entity = MapToEntity(input);

            TryToSetTenantId(entity);

            await Repository.InsertAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public override async Task<TagDto> UpdateAsync(Guid id, UpdateTagDto input)
        {
            await CheckUpdatePolicyAsync();

            var entity = await GetEntityByIdAsync(id);

            await CheckCurrentUserIsStoreOwner(entity.StoreId, CurrentUser.Id);

            MapToEntity(input, entity);
            await Repository.UpdateAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await CheckDeletePolicyAsync();

            var entity = await GetEntityByIdAsync(id);

            await CheckCurrentUserIsStoreOwner(entity.StoreId, CurrentUser.Id);

            await DeleteByIdAsync(id);
        }

        protected virtual Task CheckCurrentUserIsStoreOwner(Guid storeId, Guid? userId)
        {
            //TODO: check if current user id owner of store
            return Task.CompletedTask;
        }
    }
}
