using EasyAbp.EShop.Plugins.ProductTag.Permissions;
using EasyAbp.EShop.Plugins.ProductTag.Tags.Dtos;
using EasyAbp.EShop.Stores.Permissions;
using JetBrains.Annotations;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Linq;

namespace EasyAbp.EShop.Plugins.ProductTag.Tags
{
    public class TagAppService : CrudAppService<Tag, TagDto, Guid, GetTagListDto, CreateTagDto, UpdateTagDto>,
        ITagAppService
    {
        private readonly IAsyncQueryableExecuter _asyncQueryableExecuter;
        protected override string CreatePolicyName { get; set; } = ProductTagPermissions.Tags.Create;
        protected override string DeletePolicyName { get; set; } = ProductTagPermissions.Tags.Delete;
        protected override string UpdatePolicyName { get; set; } = ProductTagPermissions.Tags.Update;
        protected override string GetPolicyName { get; set; } = ProductTagPermissions.Tags.Default;
        protected override string GetListPolicyName { get; set; } = ProductTagPermissions.Tags.Default;

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

            await AuthorizationService.CheckStoreOwnerAsync(entity.StoreId, GetPolicyName, entity);

            return MapToGetOutputDto(entity);
        }

        public override async Task<PagedResultDto<TagDto>> GetListAsync(GetTagListDto input)
        {
            await CheckGetListPolicyAsync();

            await AuthorizationService.CheckStoreOwnerAsync(input.StoreId, GetListPolicyName);

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

            var entity = MapToEntity(input);

            await AuthorizationService.CheckStoreOwnerAsync(entity.StoreId, CreatePolicyName, entity);

            TryToSetTenantId(entity);

            await Repository.InsertAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public override async Task<TagDto> UpdateAsync(Guid id, UpdateTagDto input)
        {
            await CheckUpdatePolicyAsync();

            var entity = await GetEntityByIdAsync(id);

            await AuthorizationService.CheckStoreOwnerAsync(entity.StoreId, UpdatePolicyName, entity);

            MapToEntity(input, entity);
            await Repository.UpdateAsync(entity, autoSave: true);

            return MapToGetOutputDto(entity);
        }

        public override async Task DeleteAsync(Guid id)
        {
            await CheckDeletePolicyAsync();

            var entity = await GetEntityByIdAsync(id);

            await AuthorizationService.CheckStoreOwnerAsync(entity.StoreId, DeletePolicyName, entity);

            await DeleteByIdAsync(id);
        }
    }
}
