using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.Permissions;
using EasyAbp.EShop.Plugins.Booking.GrantedStores.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking.GrantedStores
{
    public class GrantedStoreAppService : CrudAppService<GrantedStore, GrantedStoreDto, Guid,
            GetGrantedStoreListDto, CreateUpdateGrantedStoreDto, CreateUpdateGrantedStoreDto>,
        IGrantedStoreAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;
        protected override string CreatePolicyName { get; set; } = BookingPermissions.GrantedStore.Create;
        protected override string UpdatePolicyName { get; set; } = BookingPermissions.GrantedStore.Update;
        protected override string DeletePolicyName { get; set; } = BookingPermissions.GrantedStore.Delete;

        private readonly IGrantedStoreRepository _repository;

        public GrantedStoreAppService(IGrantedStoreRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<GrantedStore>> CreateFilteredQueryAsync(GetGrantedStoreListDto input)
        {
            return (await base.CreateFilteredQueryAsync(input))
                .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId)
                .WhereIf(input.AssetId.HasValue, x => x.AssetId == input.AssetId)
                .WhereIf(input.AssetCategoryId.HasValue, x => x.AssetCategoryId == input.AssetCategoryId)
                .WhereIf(input.AllowAll.HasValue, x => x.AllowAll == input.AllowAll);
        }
    }
}
