using EasyAbp.EShop.Products.Tags;
using EasyAbp.EShop.Products.Tags.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Tags.Tag
{
    public class EditModalModel : ProductsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        [BindProperty]
        public UpdateTagDto ViewModel { get; set; }

        private readonly ITagAppService _service;

        public EditModalModel(ITagAppService service)
        {
            _service = service;
        }

        public virtual async Task OnGetAsync()
        {
            var dto = await _service.GetAsync(Id);
            ViewModel = ObjectMapper.Map<TagDto, UpdateTagDto>(dto);
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            await _service.UpdateAsync(Id, ViewModel);
            return NoContent();
        }
    }
}