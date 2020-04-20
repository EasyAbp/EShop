using System.Threading.Tasks;
using EasyAbp.EShop.Products.Categories;
using EasyAbp.EShop.Products.Categories.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Categories.Category
{
    public class CreateModalModel : ProductsPageModel
    {
        [BindProperty]
        public CreateUpdateCategoryDto Category { get; set; }

        private readonly ICategoryAppService _service;

        public CreateModalModel(ICategoryAppService service)
        {
            _service = service;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(Category);
            return NoContent();
        }
    }
}