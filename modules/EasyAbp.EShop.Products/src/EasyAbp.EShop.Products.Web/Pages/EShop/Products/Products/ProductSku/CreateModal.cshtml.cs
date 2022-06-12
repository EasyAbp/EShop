using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.ProductDetails;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku
{
    public class CreateModalModel : ProductsPageModel
    {
        [HiddenInput]
        [BindProperty(SupportsGet = true)]
        public Guid ProductId { get; set; }

        [BindProperty]
        public CreateProductSkuViewModel ProductSku { get; set; } = new CreateProductSkuViewModel();

        [BindProperty]
        public Dictionary<string, Guid> SelectedAttributeOptionIdDict { get; set; }

        public Dictionary<string, List<SelectListItem>> Attributes { get; set; }

        private readonly IProductDetailAppService _productDetailAppService;
        private readonly IProductAppService _productAppService;

        public CreateModalModel(
            IProductDetailAppService productDetailAppService,
            IProductAppService productAppService)
        {
            _productDetailAppService = productDetailAppService;
            _productAppService = productAppService;
        }

        public virtual async Task OnGetAsync()
        {
            var product = await _productAppService.GetAsync(ProductId);

            Attributes = new Dictionary<string, List<SelectListItem>>();

            foreach (var attribute in product.ProductAttributes.ToList())
            {
                Attributes.Add(attribute.DisplayName,
                    attribute.ProductAttributeOptions
                        .Select(dto => new SelectListItem(dto.DisplayName, dto.Id.ToString())).ToList());
            }
        }

        public virtual async Task<IActionResult> OnPostAsync()
        {
            var createDto = ObjectMapper.Map<CreateProductSkuViewModel, CreateProductSkuDto>(ProductSku);

            createDto.AttributeOptionIds = SelectedAttributeOptionIdDict.Values.ToList();
            if (ProductSku.ProductDetail.HasContent())
            {
                var detail = await _productDetailAppService.CreateAsync(
                    ObjectMapper.Map<CreateEditSkuProductDetailViewModel, CreateUpdateProductDetailDto>(ProductSku
                        .ProductDetail));

                createDto.ProductDetailId = detail.Id;
            }

            var product = await _productAppService.CreateSkuAsync(ProductId, createDto);
            var productSku = product.ProductSkus
                .Single(x => !x.AttributeOptionIds.Except(createDto.AttributeOptionIds).Any());

            await _productAppService.ChangeInventoryAsync(product.Id, productSku.Id, new ChangeProductInventoryDto
            {
                ChangedInventory = ProductSku.Inventory
            });

            return NoContent();
        }
    }
}