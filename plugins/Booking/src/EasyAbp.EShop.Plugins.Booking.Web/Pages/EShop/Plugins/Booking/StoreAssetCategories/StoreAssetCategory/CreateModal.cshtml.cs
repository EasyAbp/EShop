using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories;
using EasyAbp.EShop.Plugins.Booking.StoreAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.StoreAssetCategories.StoreAssetCategory.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.StoreAssetCategories.StoreAssetCategory
{
    public class CreateModalModel : BookingPageModel
    {
        [BindProperty]
        public CreateEditStoreAssetCategoryViewModel ViewModel { get; set; }

        private readonly IStoreAssetCategoryAppService _service;

        public CreateModalModel(IStoreAssetCategoryAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateEditStoreAssetCategoryViewModel, CreateUpdateStoreAssetCategoryDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}