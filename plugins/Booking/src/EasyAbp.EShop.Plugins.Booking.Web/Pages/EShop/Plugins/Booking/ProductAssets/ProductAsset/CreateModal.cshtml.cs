using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset.ViewModels;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset
{
    public class CreateModalModel : BookingPageModel
    {
        [BindProperty(SupportsGet = true)]
        public CreateProductAssetViewModel ViewModel { get; set; }

        private readonly IProductAssetAppService _service;

        public CreateModalModel(IProductAssetAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateProductAssetViewModel, CreateProductAssetDto>(ViewModel);
            await _service.CreateAsync(dto);
            return NoContent();
        }
    }
}