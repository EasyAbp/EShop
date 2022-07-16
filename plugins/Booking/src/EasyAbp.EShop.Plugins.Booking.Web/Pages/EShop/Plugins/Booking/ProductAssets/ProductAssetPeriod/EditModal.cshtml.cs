using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAssetPeriod.ViewModels;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAssetPeriod
{
    public class EditModalModel : BookingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductAssetId { get; set; }
        
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid PeriodId { get; set; }

        [BindProperty]
        public EditProductAssetPeriodViewModel ViewModel { get; set; }

        private readonly IProductAssetAppService _service;

        public EditModalModel(IProductAssetAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(ProductAssetId);
            ViewModel = ObjectMapper.Map<ProductAssetPeriodDto, EditProductAssetPeriodViewModel>(
                dto.Periods.Single(x => x.PeriodId == PeriodId));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditProductAssetPeriodViewModel, UpdateProductAssetPeriodDto>(ViewModel);
            await _service.UpdatePeriodAsync(ProductAssetId, PeriodId, dto);
            return NoContent();
        }
    }
}