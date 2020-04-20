using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.Categories.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product
{
    public class CreateModalModel : ProductsPageModel
    {
        [BindProperty]
        public CreateUpdateProductDto Product { get; set; }
        
        public IEnumerable<SelectListItem> Categories { get; set; }

        private readonly ICategoryAppService _categoryAppService;
        private readonly IProductAppService _service;

        public CreateModalModel(
            ICategoryAppService categoryAppService,
            IProductAppService service)
        {
            _categoryAppService = categoryAppService;
            _service = service;
        }

        public async Task OnGetAsync(Guid? storeId)
        {
            Categories =
                (await _categoryAppService.GetListAsync(new PagedAndSortedResultRequestDto
                    {MaxResultCount = LimitedResultRequestDto.MaxMaxResultCount}))?.Items
                .Select(dto => new SelectListItem(dto.DisplayName, dto.Id.ToString()));
            
            Product = new CreateUpdateProductDto
            {
                StoreId = storeId
            };
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(Product);
            return NoContent();
        }
    }
}