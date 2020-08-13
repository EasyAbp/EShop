using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwnerAppService : CrudAppService<StoreOwner, StoreOwnerDto, Guid, GetStoreOwnerListDto>,
        IStoreOwnerAppService
    {
        protected override string CreatePolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string DeletePolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string UpdatePolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string GetPolicyName { get; set; } = StoresPermissions.Stores.Manage;
        protected override string GetListPolicyName { get; set; } = StoresPermissions.Stores.Manage;

        private readonly IStoreOwnerRepository _repository;

        public StoreOwnerAppService(IStoreOwnerRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public override async Task<StoreOwnerDto> CreateAsync(StoreOwnerDto input)
        {
            if (await _repository.IsExistAsync(input.StoreId, input.OwnerId))
            {
                throw new StoreOwnerDuplicatedException(input.StoreId, input.OwnerId);
            }

            return await base.CreateAsync(input);
        }

        protected override IQueryable<StoreOwner> CreateFilteredQuery(GetStoreOwnerListDto input)
        {
            var queryable = Repository.AsQueryable();

            if (input.StoreId.HasValue)
            {
                queryable = queryable.Where(x => x.StoreId == input.StoreId);
            }

            if (input.OwnerId.HasValue)
            {
                queryable = queryable.Where(x => x.OwnerId == input.OwnerId);
            }

            return queryable;
        }
    }
}