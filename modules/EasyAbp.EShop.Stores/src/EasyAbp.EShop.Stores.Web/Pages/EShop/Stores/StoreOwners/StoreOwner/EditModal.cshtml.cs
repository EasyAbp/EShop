using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner
{
    public class EditModalModel : StoresPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty] public CreateEditStoreOwnerViewModel StoreOwner { get; set; }

        private readonly IStoreOwnerAppService _storeOwnerAppService;

        public EditModalModel(IStoreOwnerAppService storeOwnerAppService)
        {
            _storeOwnerAppService = storeOwnerAppService;
        }

        public async Task OnGetAsync()
        {
            var dto = await _storeOwnerAppService.GetAsync(Id);
            StoreOwner = ObjectMapper.Map<StoreOwnerDto, CreateEditStoreOwnerViewModel>(dto);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _storeOwnerAppService.UpdateAsync(Id,
                ObjectMapper.Map<CreateEditStoreOwnerViewModel, CreateUpdateStoreOwnerDto>(StoreOwner));
            return NoContent();
        }
    }
}