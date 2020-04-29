using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku
{
    public class EditModalModel : ProductsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid StoreId { get; set; }
        
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductId { get; set; }
        
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductSkuId { get; set; }

        [BindProperty]
        public CreateEditProductSkuViewModel ProductSku { get; set; }

        private readonly IProductAppService _productAppService;

        public EditModalModel(IProductAppService productAppService)
        {
            _productAppService = productAppService;
        }

        public virtual async Task OnGetAsync()
        {
            var product = await _productAppService.GetAsync(ProductId);

            ProductSku =
                ObjectMapper.Map<ProductSkuDto, CreateEditProductSkuViewModel>(
                    product.ProductSkus.Single(x => x.Id == ProductSkuId));
        }
        
        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _productAppService.UpdateSkuAsync(ProductId, ProductSkuId, StoreId,
                ObjectMapper.Map<CreateEditProductSkuViewModel, UpdateProductSkuDto>(ProductSku));
            
            return NoContent();
        }
    }
}