using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public class StoreOwnerAppService : ReadOnlyAppService<StoreOwner, StoreOwnerDto, Guid, GetStoreOwnerListDto>,
        IStoreOwnerAppService
    {
        protected override string GetListPolicyName { get; set; } = StoresPermissions.Stores.Default;

        private readonly IStoreOwnerRepository _repository;

        public StoreOwnerAppService(IStoreOwnerRepository repository) : base(repository)
        {
            _repository = repository;
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

        [RemoteService(false)]
        public override Task<StoreOwnerDto> GetAsync(Guid id)
        {
            throw new NotSupportedException();
        }

        [RemoteService(false)]
        public async Task<bool> IsStoreOwnerAsync(Guid storeId, Guid userId)
        {
            var storeOwner = await _repository.FindAsync(x => x.OwnerId == userId && x.StoreId == storeId, false);

            return storeOwner != null;
        }
    }
}
