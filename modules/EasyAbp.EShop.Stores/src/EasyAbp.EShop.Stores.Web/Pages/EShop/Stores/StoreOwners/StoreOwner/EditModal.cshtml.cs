using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Stores.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner.ViewModels;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.StoreOwners.StoreOwner
{
    public class EditModalModel : StoresPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty] public CreateEditStoreOwnerViewModel StoreOwner { get; set; }

        private readonly IStoreAppService _storeService;
        private readonly IIdentityUserAppService _userAppService;
        private readonly IStoreOwnerAppService _storeOwnerAppService;

        public EditModalModel(IStoreAppService storeService,
            IIdentityUserAppService userAppService,
            IStoreOwnerAppService storeOwnerAppService)
        {
            _storeService = storeService;
            _userAppService = userAppService;
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
                ObjectMapper.Map<CreateEditStoreOwnerViewModel, StoreOwnerDto>( StoreOwner));
            return NoContent();
        }
    }
}