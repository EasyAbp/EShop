using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Localization;
using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwnerAppService : MultiStoreCrudAppService<StoreOwner, StoreOwnerDto, Guid, GetStoreOwnerListDto, CreateUpdateStoreOwnerDto, CreateUpdateStoreOwnerDto>,
        IStoreOwnerAppService
    {
        protected override string CreatePolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string DeletePolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string UpdatePolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string GetPolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string GetListPolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string CrossStorePolicyName { get; set; } = StoresPermissions.Stores.CrossStore;

        private readonly IStoreOwnerRepository _repository;
        private readonly IExternalUserLookupServiceProvider _externalUserLookupServiceProvider;

        public StoreOwnerAppService(
            IStoreOwnerRepository repository,
            IExternalUserLookupServiceProvider externalUserLookupServiceProvider)
            : base(repository)
        {
            _repository = repository;
            _externalUserLookupServiceProvider = externalUserLookupServiceProvider;

            LocalizationResource = typeof(StoresResource);
            ObjectMapperContext = typeof(EShopStoresApplicationModule);
        }

        public override async Task<StoreOwnerDto> CreateAsync(CreateUpdateStoreOwnerDto input)
        {
            if (await _repository.IsExistAsync(input.StoreId, input.OwnerUserId))
            {
                throw new StoreOwnerDuplicatedException(input.StoreId, input.OwnerUserId);
            }

            return await base.CreateAsync(input);
        }

        public override async Task<StoreOwnerDto> GetAsync(Guid id)
        {
            var dto = await base.GetAsync(id);
            
            var userData = await _externalUserLookupServiceProvider.FindByIdAsync(dto.OwnerUserId);

            if (userData != null)
            {
                dto.OwnerUserName = userData.UserName;
            }

            return dto;
        }

        public override async Task<PagedResultDto<StoreOwnerDto>> GetListAsync(GetStoreOwnerListDto input)
        {
            var result = await base.GetListAsync(input);

            foreach (var dto in result.Items)
            {
                var userData = await _externalUserLookupServiceProvider.FindByIdAsync(dto.OwnerUserId);

                if (userData != null)
                {
                    dto.OwnerUserName = userData.UserName;
                }
            }

            return result;
        }

        protected override async Task<IQueryable<StoreOwner>> CreateFilteredQueryAsync(GetStoreOwnerListDto input)
        {
            var queryable = await Repository.GetQueryableAsync();

            if (input.OwnerUserId.HasValue)
            {
                queryable = queryable.Where(x => x.OwnerUserId == input.OwnerUserId);
            }

            if (input.StoreId.HasValue)
            {
                queryable = queryable.Where(x => x.StoreId == input.StoreId);
            }

            return queryable;
        }
    }
}