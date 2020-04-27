using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Stores.Stores
{
    public class StoreAppService : CrudAppService<Store, StoreDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateStoreDto, CreateUpdateStoreDto>,
        IStoreAppService
    {
        private readonly IStoreRepository _repository;

        public StoreAppService(IStoreRepository repository) : base(repository)
        {
            _repository = repository;
        }

        public async Task<StoreDto> GetDefaultAsync()
        {
            // Todo: need to be improved
            return ObjectMapper.Map<Store, StoreDto>(await _repository.FirstOrDefaultAsync());
        }
    }
}