using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Stores.Dtos;
using EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.StoreOwners;
using EasyAbp.EShop.Stores.StoreOwners.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Identity;

namespace EasyAbp.EShop.Stores.Web.Pages.EShop.Stores.Stores.Store
{
    public class EditModalModel : StoresPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditStoreViewModel Store { get; set; }

        public ICollection<SelectListItem> StoreOwners { get; set; }

        private readonly IStoreAppService _service;
        private readonly IIdentityUserAppService _userAppService;
        private readonly IStoreOwnerAppService _storeOwnerAppService;

        public EditModalModel(IStoreAppService service,
            IIdentityUserAppService userAppService,
            IStoreOwnerAppService storeOwnerAppService)
        {
            _service = service;
            _userAppService = userAppService;
            _storeOwnerAppService = storeOwnerAppService;
        }

        public async Task OnGetAsync()
        {
            StoreOwners =
                (await _userAppService.GetListAsync(new GetIdentityUsersInput
                { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount })).Items
                .Select(x => new SelectListItem(x.UserName, x.Id.ToString())).ToList();

            var dto = await _service.GetAsync(Id);
            Store = ObjectMapper.Map<StoreDto, CreateEditStoreViewModel>(dto);

            Store.OwnerIds = (await _storeOwnerAppService.GetListAsync(new GetStoreOwnerListDto
            {
                StoreId = Id,
                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
            })).Items.Select(x => x.OwnerId).ToList();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, ObjectMapper.Map<CreateEditStoreViewModel, CreateUpdateStoreDto>(Store));
            return NoContent();
        }
    }
}