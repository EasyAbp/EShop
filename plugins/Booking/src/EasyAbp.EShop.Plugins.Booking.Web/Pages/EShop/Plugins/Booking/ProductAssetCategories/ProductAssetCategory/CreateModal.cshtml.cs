using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategory.ViewModels;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategory
{
    public class CreateModalModel : BookingPageModel
    {
        [BindProperty(SupportsGet = true)]
        public CreateProductAssetCategoryViewModel ViewModel { get; set; }

        private readonly IProductAssetCategoryAppService _service;

        public CreateModalModel(IProductAssetCategoryAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateProductAssetCategoryViewModel, CreateProductAssetCategoryDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}