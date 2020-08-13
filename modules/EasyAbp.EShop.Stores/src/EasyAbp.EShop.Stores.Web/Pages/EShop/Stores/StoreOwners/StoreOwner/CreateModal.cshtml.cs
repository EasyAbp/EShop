using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner
{
    public class CreateModalModel : StoresPageModel
    {
        [BindProperty]
        public CreateEditStoreOwnerViewModel StoreOwner { get; set; }

        private readonly IStoreAppService _storeService;
        private readonly IIdentityUserAppService _userAppService;
        private readonly IStoreOwnerAppService _storeOwnerAppService;

        public CreateModalModel(IStoreAppService storeService,
            IIdentityUserAppService userAppService,
            IStoreOwnerAppService storeOwnerAppService)
        {
            _storeService = storeService;
            _userAppService = userAppService;
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