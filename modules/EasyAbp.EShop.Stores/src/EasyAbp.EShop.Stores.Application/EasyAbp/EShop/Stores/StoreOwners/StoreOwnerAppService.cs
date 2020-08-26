using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwnerAppService : MultiStoreCrudAppService<StoreOwner, StoreOwnerDto, Guid, GetStoreOwnerListDto>,
        IStoreOwnerAppService
    {
        protected override string CreatePolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string DeletePolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string UpdatePolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string GetPolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string GetListPolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string CrossStorePolicyName { get; set; } = StoresPermissions.Stores.CrossStore;

        private readonly IStoreOwnerRepository _repository;
        private readonly IStoreOwnerStore _storeOwnerStore;

        public StoreOwnerAppService(IStoreOwnerRepository repository,
            IStoreOwnerStore storeOwnerStore) : base(repository)
        {
            _repository = repository;
            _storeOwnerStore = storeOwnerStore;
        }

        public override async Task<StoreOwnerDto> CreateAsync(StoreOwnerDto input)
        {
            if (await _repository.IsExistAsync(input.StoreId, input.OwnerUserId))
            {
                throw new StoreOwnerDuplicatedException(input.StoreId, input.OwnerUserId);
            }

            return await base.CreateAsync(input);
        }
        
        protected override IQueryable<StoreOwner> CreateFilteredQuery(GetStoreOwnerListDto input)
        {
            var queryable = Repository.AsQueryable();

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