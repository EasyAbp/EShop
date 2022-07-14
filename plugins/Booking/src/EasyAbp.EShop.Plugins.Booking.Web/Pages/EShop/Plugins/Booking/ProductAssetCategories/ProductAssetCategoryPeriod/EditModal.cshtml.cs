using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories;
using EasyAbp.EShop.Plugins.Booking.ProductAssetCategories.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategoryPeriod.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssetCategories.ProductAssetCategoryPeriod
{
    public class EditModalModel : BookingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductAssetCategoryId { get; set; }
        
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid PeriodId { get; set; }

        [BindProperty]
        public EditProductAssetCategoryPeriodViewModel ViewModel { get; set; }

        private readonly IProductAssetCategoryAppService _service;

        public EditModalModel(IProductAssetCategoryAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(ProductAssetCategoryId);
            ViewModel = ObjectMapper.Map<ProductAssetCategoryPeriodDto, EditProductAssetCategoryPeriodViewModel>(
                dto.Periods.Single(x => x.PeriodId == PeriodId));
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditProductAssetCategoryPeriodViewModel, UpdateProductAssetCategoryPeriodDto>(ViewModel);
            await _service.UpdatePeriodAsync(ProductAssetCategoryId, PeriodId, dto);
            return NoContent();
        }
    }
}