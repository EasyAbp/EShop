using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Categories;
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
        private readonly IProductAppService _service;

        public EditModalModel(
            IProductTypeAppService productTypeAppService,
            ICategoryAppService categoryAppService,
            IProductAppService service)
        {
            _productTypeAppService = productTypeAppService;
            _categoryAppService = categoryAppService;
            _service = service;
        }

        public virtual async Task OnGetAsync(Guid storeId)
        {
            ProductTypes =
                (await _productTypeAppService.GetListAsync(new PagedAndSortedResultRequestDto
                    {MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount})).Items
                .Select(dto => new SelectListItem(dto.DisplayName, dto.Id.ToString())).ToList();
            
            Categories =
                (await _categoryAppService.GetListAsync(new PagedAndSortedResultRequestDto
                    {MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount}))?.Items
                .Select(dto => new SelectListItem(dto.DisplayName, dto.Id.ToString())).ToList();

            var productDto = await _service.GetAsync(Id);
            
            Product = ObjectMapper.Map<ProductDto, CreateEditProductViewModel>(productDto);

            Product.StoreId = storeId;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id,
                ObjectMapper.Map<CreateEditProductViewModel, CreateUpdateProductDto>(Product));
            return NoContent();
        }
    }
}