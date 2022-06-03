using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories;
using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.StoreAssetCategories.StoreAssetCategory.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.StoreAssetCategories.StoreAssetCategory
{
    public class EditModalModel : BookingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditStoreAssetCategoryViewModel ViewModel { get; set; }

        private readonly IStoreAssetCategoryAppService _service;

        public EditModalModel(IStoreAssetCategoryAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<StoreAssetCategoryDto, CreateEditStoreAssetCategoryViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditStoreAssetCategoryViewModel, CreateUpdateStoreAssetCategoryDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}