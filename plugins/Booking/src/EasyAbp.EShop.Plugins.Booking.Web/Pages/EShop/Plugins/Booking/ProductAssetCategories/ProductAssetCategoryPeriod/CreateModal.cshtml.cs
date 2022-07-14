using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategoryPeriod.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategoryPeriod
{
    public class CreateModalModel : BookingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductAssetCategoryId { get; set; }
        
        [BindProperty]
        public CreateProductAssetCategoryPeriodViewModel ViewModel { get; set; }

        private readonly IProductAssetCategoryAppService _service;

        public CreateModalModel(IProductAssetCategoryAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<CreateProductAssetCategoryPeriodViewModel, CreateProductAssetCategoryPeriodDto>(ViewModel);
            await _service.CreatePeriodAsync(ProductAssetCategoryId, dto);
            return NoContent();
        }
    }
}