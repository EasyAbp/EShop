using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.Categories.Dtos;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product
{
    public class CreateModalModel : ProductsPageModel
    {
        [BindProperty(SupportsGet = true)]
        public Guid StoreId { get; set; }

        [BindProperty]
        public CreateEditProductViewModel Product { get; set; }

        public ICollection<SelectListItem> ProductGroups { get; set; }

        public ICollection<SelectListItem> Categories { get; set; }

        private readonly ICategoryAppService _categoryAppService;
        private readonly IProductDetailAppService _productDetailAppService;
        private readonly IProductAppService _service;

        public CreateModalModel(
            ICategoryAppService categoryAppService,
            IProductDetailAppService productDetailAppService,
            IProductAppService service)
        {
            _categoryAppService = categoryAppService;
            _productDetailAppService = productDetailAppService;
            _service = service;
        }

        public virtual async Task OnGetAsync(Guid? categoryId)
        {
            ProductGroups =
                (await _service.GetProductGroupListAsync()).Items
                .Select(dto => new SelectListItem(dto.DisplayName, dto.Name)).ToList();

            Categories =
                (await _categoryAppService.GetListAsync(new GetCategoryListDto
                { MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount }))?.Items
                .Select(dto => new SelectListItem(dto.DisplayName, dto.Id.ToString())).ToList();

            Product = new CreateEditProductViewModel
            {
                StoreId = StoreId,
                ProductDetail = new CreateEditProductDetailViewModel
                {
                    StoreId = StoreId
                }
            };

            if (categoryId.HasValue)
            {
                Product.CategoryIds = new List<Guid>(new[] { categoryId.Value });
            }
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var detail = await _productDetailAppService.CreateAsync(
                ObjectMapper
                    .Map<CreateEditProductDetailViewModel, CreateUpdateProductDetailDto>(Product.ProductDetail));

            var createDto = ObjectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product);

            createDto.ProductDetailId = detail.Id;

            var product = await _service.CreateAsync(createDto);

            return NoContent();
        }
    }
}