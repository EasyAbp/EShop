using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.Permissions;
using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories
{
    public class StoreAssetCategoryAppService : CrudAppService<StoreAssetCategory, StoreAssetCategoryDto, Guid,
            GetStoreAssetCategoryListDto, CreateUpdateStoreAssetCategoryDto, CreateUpdateStoreAssetCategoryDto>,
        IStoreAssetCategoryAppService
    {
        protected override string GetPolicyName { get; set; } = null;
        protected override string GetListPolicyName { get; set; } = null;
        protected override string CreatePolicyName { get; set; } = BookingPermissions.StoreAssetCategory.Create;
        protected override string UpdatePolicyName { get; set; } = BookingPermissions.StoreAssetCategory.Update;
        protected override string DeletePolicyName { get; set; } = BookingPermissions.StoreAssetCategory.Delete;

        private readonly IStoreAssetCategoryRepository _repository;

        public StoreAssetCategoryAppService(IStoreAssetCategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }

        protected override async Task<IQueryable<StoreAssetCategory>> CreateFilteredQueryAsync(GetStoreAssetCategoryListDto input)
        {
            return (await base.CreateFilteredQueryAsync(input))
                .WhereIf(input.StoreId.HasValue, x => x.StoreId == input.StoreId)
                .WhereIf(input.AssetCategoryId.HasValue, x => x.AssetCategoryId == input.AssetCategoryId);
        }
    }
}
