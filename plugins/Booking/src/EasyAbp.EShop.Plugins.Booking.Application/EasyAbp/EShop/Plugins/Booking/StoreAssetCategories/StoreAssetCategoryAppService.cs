using System;
using EasyAbp.EShop.Plugins.Booking.Permissions;
using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Booking.StoreAssetCategories
{
    public class StoreAssetCategoryAppService : CrudAppService<StoreAssetCategory, StoreAssetCategoryDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStoreAssetCategoryDto, CreateUpdateStoreAssetCategoryDto>,
        IStoreAssetCategoryAppService
    {
        protected override string GetPolicyName { get; set; } = BookingPermissions.StoreAssetCategory.Default;
        protected override string GetListPolicyName { get; set; } = BookingPermissions.StoreAssetCategory.Default;
        protected override string CreatePolicyName { get; set; } = BookingPermissions.StoreAssetCategory.Create;
        protected override string UpdatePolicyName { get; set; } = BookingPermissions.StoreAssetCategory.Update;
        protected override string DeletePolicyName { get; set; } = BookingPermissions.StoreAssetCategory.Delete;

        private readonly IStoreAssetCategoryRepository _repository;

        public StoreAssetCategoryAppService(IStoreAssetCategoryRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
