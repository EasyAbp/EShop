using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Stores.StoreOwners
{
    public interface IStoreOwnerAppService :
        IReadOnlyAppService<
            StoreOwnerDto,
            Guid,
            GetStoreOwnerListDto>
    {
        Task<bool> IsStoreOwnerAsync(Guid storeId, Guid userId);
    }
}
