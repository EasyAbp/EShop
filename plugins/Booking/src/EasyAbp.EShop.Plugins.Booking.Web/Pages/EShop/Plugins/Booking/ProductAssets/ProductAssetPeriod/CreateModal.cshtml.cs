using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAssetPeriod.ViewModels;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAssetPeriod
{
    public class CreateModalModel : BookingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductAssetId { get; set; }
        
        [BindProperty]
        public CreateProductAssetPeriodViewModel ViewModel { get; set; }

        private readonly IProductAssetAppService _service;

        public CreateModalModel(IProductAssetAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateProductAssetPeriodViewModel, CreateProductAssetPeriodDto>(ViewModel);
            await _service.CreatePeriodAsync(ProductAssetId, dto);
            return NoContent();
        }
    }
}