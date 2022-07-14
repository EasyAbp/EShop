using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Plugins.Booking.ProductAssets;
using EasyAbp.EShop.Plugins.Booking.ProductAssets.Dtos;
using EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset.ViewModels;

namespace EasyAbp.EShop.Plugins.Booking.Web.Pages.EShop.Plugins.Booking.ProductAssets.ProductAsset
{
    public class EditModalModel : BookingPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public EditProductAssetViewModel ViewModel { get; set; }

        private readonly IProductAssetAppService _service;

        public EditModalModel(IProductAssetAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<ProductAssetDto, EditProductAssetViewModel>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var dto = ObjectMapper.Map<EditProductAssetViewModel, UpdateProductAssetDto>(ViewModel);
            await _service.UpdateAsync(Id, dto);
            return NoContent();
        }
    }
}