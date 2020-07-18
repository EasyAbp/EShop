using EasyAbp.EShop.Products.Tags;
using EasyAbp.EShop.Products.Tags.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Tags.Tag
{
    public class CreateModalModel : ProductsPageModel
    {
        [BindProperty]
        public CreateTagDto ViewModel { get; set; }

        private readonly ITagAppService _service;

        public CreateModalModel(ITagAppService service)
        {
            _service = service;
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.CreateAsync(ViewModel);
            return NoContent();
        }
    }
}