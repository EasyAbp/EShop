using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku
{
    public class ChangeInventoryModal : ProductsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductId { get; set; }

        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductSkuId { get; set; }

        [BindProperty]
        public ChangeProductInventoryViewModel ViewModel { get; set; }

        private readonly IProductAppService _service;

        public ChangeInventoryModal(IProductAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var product = await _service.GetAsync(ProductId);
            product.GetSkuById(ProductSkuId); // ensure the specified sku exists.

            ViewModel = new ChangeProductInventoryViewModel
            {
                ChangedInventory = 0,
                ProductInventoryChangeType = InventoryChangeType.IncreaseInventory
            };
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.ChangeInventoryAsync(ProductId, ProductSkuId, new ChangeProductInventoryDto
            {
                ChangedInventory = ViewModel.ProductInventoryChangeType == InventoryChangeType.IncreaseInventory
                    ? ViewModel.ChangedInventory
                    : -ViewModel.ChangedInventory
            });

            return NoContent();
        }
    }
}