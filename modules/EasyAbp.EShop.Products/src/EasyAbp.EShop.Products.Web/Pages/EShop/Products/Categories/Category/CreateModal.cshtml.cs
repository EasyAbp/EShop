using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.Categories.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Categories.Category.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Categories.Category
{
    public class CreateModalModel : ProductsPageModel
    {
        [BindProperty]
        public CreateEditCategoryViewModel Category { get; set; }

        [BindProperty(SupportsGet = true)]
        public Guid? ParentId { get; set; }
        
        private readonly ICategoryAppService _service;

        public CreateModalModel(ICategoryAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            Category = new CreateEditCategoryViewModel
            {
                ParentId = ParentId
            };

            await Task.CompletedTask;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(
                ObjectMapper.Map<CreateEditCategoryViewModel, CreateUpdateCategoryDto>(Category));
            
            return NoContent();
        }
    }
}