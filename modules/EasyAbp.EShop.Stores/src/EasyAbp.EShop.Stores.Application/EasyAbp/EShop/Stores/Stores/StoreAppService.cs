using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Stores.Stores.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreAppService : CrudAppService<Store, StoreDto, Guid, PagedAndSortedResultRequestDto,
            CreateUpdateStoreDto, CreateUpdateStoreDto>,
        IStoreAppService
    {
        protected override string CreatePolicyName { get; set; } = StoresPermissions.Stores.Create;
        protected override string DeletePolicyName { get; set; } = StoresPermissions.Stores.Delete;
        protected override string UpdatePolicyName { get; set; } = StoresPermissions.Stores.Update;
        protected override string GetPolicyName { get; set; } = StoresPermissions.Stores.Default;
        protected override string GetListPolicyName { get; set; } = StoresPermissions.Stores.Default;

        private readonly IStoreRepository _repository;

        public StoreAppService(IStoreRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<StoreDto> GetDefaultAsync()
        {
            // Todo: need to be improved
            return ObjectMapper.Map<Store, StoreDto>(await _repository.FindDefaultStoreAsync());
        }
    }
}