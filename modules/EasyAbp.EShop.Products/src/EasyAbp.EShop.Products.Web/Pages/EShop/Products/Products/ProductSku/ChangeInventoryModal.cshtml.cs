using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductInventories;
using EasyAbp.EShop.Products.ProductInventories.Dtos;
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

        private readonly IProductInventoryAppService _service;

        public ChangeInventoryModal(IProductInventoryAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(ProductId, ProductSkuId);
            
            ViewModel = new ChangeProductInventoryViewModel
            {
                ChangedInventory = 0,
                ProductInventoryChangeType = InventoryChangeType.IncreaseInventory
            };
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(new UpdateProductInventoryDto
            {
                ProductId = ProductId,
                ProductSkuId = ProductSkuId,
                ChangedInventory = ViewModel.ProductInventoryChangeType == InventoryChangeType.IncreaseInventory
                    ? ViewModel.ChangedInventory
                    : -ViewModel.ChangedInventory
            });
            
            return NoContent();
        }
    }
}