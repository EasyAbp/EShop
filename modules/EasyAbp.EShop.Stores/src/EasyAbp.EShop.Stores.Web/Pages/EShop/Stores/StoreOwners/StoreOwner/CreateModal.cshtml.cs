using System.Threading.Tasks;
using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner
{
    public class CreateModalModel : StoresPageModel
    {
        [BindProperty]
        public CreateEditStoreOwnerViewModel StoreOwner { get; set; }

        private readonly IStoreOwnerAppService _storeOwnerAppService;

        public CreateModalModel(IStoreOwnerAppService storeOwnerAppService)
        {
            _storeOwnerAppService = storeOwnerAppService;
        }

        public virtual Task OnGetAsync()
        {
            return Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var createDto = ObjectMapper.Map<CreateEditStoreOwnerViewModel, StoreOwnerDto>(StoreOwner);
            var storeOwner = await _storeOwnerAppService.CreateAsync(createDto);

            return NoContent();
        }
    }
}