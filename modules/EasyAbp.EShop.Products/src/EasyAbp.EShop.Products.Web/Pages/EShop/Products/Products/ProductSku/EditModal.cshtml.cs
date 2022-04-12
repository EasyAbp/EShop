using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku
{
    public class EditModalModel : ProductsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductId { get; set; }
        
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductSkuId { get; set; }

        [BindProperty]
        public EditProductSkuViewModel ProductSku { get; set; }

        private readonly IProductAppService _productAppService;
        private readonly IProductDetailAppService _productDetailAppService;

        public EditModalModel(
            IProductAppService productAppService,
            IProductDetailAppService productDetailAppService)
        {
            _productAppService = productAppService;
            _productDetailAppService = productDetailAppService;
        }

        public virtual async Task OnGetAsync()
        {
            var product = await _productAppService.GetAsync(ProductId);
            var sku = product.ProductSkus.Single(x => x.Id == ProductSkuId);

            ProductSku = ObjectMapper.Map<ProductSkuDto, EditProductSkuViewModel>(sku);
            
            if (sku.ProductDetailId.HasValue)
            {
                var detailDto = await _productDetailAppService.GetAsync(sku.ProductDetailId.Value);

                ProductSku.ProductDetail = new CreateEditSkuProductDetailViewModel
                {
                    StoreId = detailDto.StoreId,
                    Description = detailDto.Description
                };
            }
        }
        
        public virtual async Task<IActionResult> OnPostAsync()
        {
            var originalProduct = await _productAppService.GetAsync(ProductId);
            var originalSku = originalProduct.ProductSkus.Single(x => x.Id == ProductSkuId);

            var updateProductSkuDto = ObjectMapper.Map<EditProductSkuViewModel, UpdateProductSkuDto>(ProductSku);
            
            if (ProductSku.ProductDetail.HasContent())
            {
                if (originalSku.ProductDetailId.HasValue)
                {
                    var detail = await _productDetailAppService.GetAsync(originalSku.ProductDetailId.Value);

                    await _productDetailAppService.UpdateAsync(detail.Id,
                        ObjectMapper.Map<CreateEditSkuProductDetailViewModel, CreateUpdateProductDetailDto>(
                            ProductSku.ProductDetail));
                    
                    updateProductSkuDto.ProductDetailId = detail.Id;
                }
                else
                {
                    var detail = await _productDetailAppService.CreateAsync(
                        ObjectMapper.Map<CreateEditSkuProductDetailViewModel, CreateUpdateProductDetailDto>(
                            ProductSku.ProductDetail));

                    updateProductSkuDto.ProductDetailId = detail.Id;
                }
            }
            else if (originalSku.ProductDetailId.HasValue)
            {
                await _productDetailAppService.DeleteAsync(originalSku.ProductDetailId.Value);
                
                updateProductSkuDto.ProductDetailId = null;
            }
            
            await _productAppService.UpdateSkuAsync(ProductId, ProductSkuId, updateProductSkuDto);
            
            return NoContent();
        }
    }
}