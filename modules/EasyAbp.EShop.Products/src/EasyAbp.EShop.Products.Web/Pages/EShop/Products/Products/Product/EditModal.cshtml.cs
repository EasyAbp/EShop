using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.Categories.Dtos;
using EasyAbp.EShop.Products.ProductCategories;
using EasyAbp.EShop.Products.ProductCategories.Dtos;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using Microsoft.AspNetCore.Mvc;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.ProductTypes;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product
{
    public class EditModalModel : ProductsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public CreateEditProductViewModel Product { get; set; }
        
        public ICollection<SelectListItem> ProductTypes { get; set; }
        
        public ICollection<SelectListItem> Categories { get; set; }

        private readonly IProductTypeAppService _productTypeAppService;
        private readonly ICategoryAppService _categoryAppService;
        private readonly IProductDetailAppService _productDetailAppService;
        private readonly IProductCategoryAppService _productCategoryAppService;
        private readonly IProductAppService _service;

        public EditModalModel(
            IProductTypeAppService productTypeAppService,
            ICategoryAppService categoryAppService,
            IProductDetailAppService productDetailAppService,
            IProductCategoryAppService productCategoryAppService,
            IProductAppService service)
        {
            _productTypeAppService = productTypeAppService;
            _categoryAppService = categoryAppService;
            _productDetailAppService = productDetailAppService;
            _productCategoryAppService = productCategoryAppService;
            _service = service;
        }

        public virtual async Task OnGetAsync(Guid storeId)
        {
            ProductTypes =
                (await _productTypeAppService.GetListAsync(new PagedAndSortedResultRequestDto
                    {MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount})).Items
                .Select(dto => new SelectListItem(dto.DisplayName, dto.Id.ToString())).ToList();
            
            Categories =
                (await _categoryAppService.GetListAsync(new GetCategoryListDto
                    {MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount}))?.Items
                .Select(dto => new SelectListItem(dto.DisplayName, dto.Id.ToString())).ToList();

            var productDto = await _service.GetAsync(Id, storeId);

            var detailDto = await _productDetailAppService.GetAsync(productDto.ProductDetailId);
            
            Product = ObjectMapper.Map<ProductDto, CreateEditProductViewModel>(productDto);

            Product.CategoryIds = (await _productCategoryAppService.GetListAsync(new GetProductCategoryListDto
            {
                ProductId = productDto.Id,
                MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount
            })).Items.Select(x => x.CategoryId).ToList();
            
            Product.ProductDetail = new CreateEditProductDetailViewModel
            {
                StoreId = storeId,
                Description = detailDto.Description
            };

            Product.StoreId = storeId;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var product = await _service.GetAsync(Id, Product.StoreId);

            var detail = await _productDetailAppService.GetAsync(product.ProductDetailId);

            await _productDetailAppService.UpdateAsync(detail.Id,
                ObjectMapper
                    .Map<CreateEditProductDetailViewModel, CreateUpdateProductDetailDto>(Product.ProductDetail));

            var updateProductDto = ObjectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product);

            updateProductDto.ProductDetailId = detail.Id;
            
            await _service.UpdateAsync(Id, updateProductDto);
            return NoContent();
        }
    }
}