using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Stores.Stores.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreAppService : CrudAppService<Store, StoreDto, Guid, GetStoreListInput, CreateUpdateStoreDto, CreateUpdateStoreDto>, IStoreAppService
    {
        protected override string CreatePolicyName { get; set; } = StoresPermissions.Stores.Create;
        protected override string DeletePolicyName { get; set; } = StoresPermissions.Stores.Delete;
        protected override string UpdatePolicyName { get; set; } = StoresPermissions.Stores.Update;
        protected override string GetPolicyName { get; set; } = StoresPermissions.Stores.Default;
        protected override string GetListPolicyName { get; set; } = StoresPermissions.Stores.Default;

        private readonly IPermissionChecker _permissionChecker;
        private readonly IStoreRepository _repository;

        public StoreAppService(
            IPermissionChecker permissionChecker,
            IStoreRepository repository) : base(repository)
        {
            _permissionChecker = permissionChecker;
            _repository = repository;
        }

        protected override async Task<IQueryable<Store>> CreateFilteredQueryAsync(GetStoreListInput input)
        {
            if (!input.OnlyManageable || await _permissionChecker.IsGrantedAsync(StoresPermissions.Stores.CrossStore))
            {
                return _repository.AsQueryable();
            }

            return await _repository.GetQueryableOnlyOwnStoreAsync(CurrentUser.GetId());
        }

        public override async Task<PagedResultDto<StoreDto>> GetListAsync(GetStoreListInput input)
        {
            await CheckGetListPolicyAsync();

            var query = await CreateFilteredQueryAsync(input);

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);

            var entities = await AsyncExecuter.ToListAsync(query);
            var entityDtos = await MapToGetListOutputDtosAsync(entities);

            return new PagedResultDto<StoreDto>(
                totalCount,
                entityDtos
            );
        }

        public async Task<StoreDto> GetDefaultAsync()
        {
            // Todo: need to be improved
            return ObjectMapper.Map<Store, StoreDto>(await _repository.FindDefaultStoreAsync());
        }
    }
}