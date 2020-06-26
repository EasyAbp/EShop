using System;
using System.Collections.Generic;
using System.Linq;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Categories.Dtos;
using AutoMapper;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.Product.ViewModels;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Products.ProductSku.ViewModels;
using Volo.Abp.AutoMapper;

namespace EasyAbp.EShop.Products.Web
{
    public class ProductsWebAutoMapperProfile : Profile
    {
        public ProductsWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<ProductDto, CreateEditProductViewModel>()
                .Ignore(model => model.CategoryIds)
                .Ignore(model => model.ProductDetail)
                .Ignore(model => model.StoreId)
                .ForSourceMember(dto => dto.Sold, opt => opt.DoNotValidate())
                .ForSourceMember(dto => dto.ProductDetailId, opt => opt.DoNotValidate())
                // .Ignore(x => x.ProductAttributes);
                .ForMember(dest => dest.ProductAttributeNames,
                    opt => opt.MapFrom(source =>
                        source.ProductAttributes.Select(x => x.DisplayName).JoinAsString(",")))
                .ForMember(dest => dest.ProductAttributeOptionNames,
                    opt => opt.MapFrom(x =>
                        x.ProductAttributes
                            .Select(a => a.ProductAttributeOptions.Select(o => o.DisplayName).JoinAsString(","))
                            .JoinAsString(Environment.NewLine)));
            CreateMap<CreateEditProductViewModel, CreateUpdateProductDto>()
                // Todo: should be remove
                .Ignore(dto => dto.TagIds)
                .Ignore(dto => dto.ProductDetailId)
                .ForSourceMember(model => model.ProductDetail, opt => opt.DoNotValidate())
                .ForMember(dest => dest.ProductAttributes,
                    opt => opt.MapFrom(x =>
                        x.ProductAttributeNames.Split(",", StringSplitOptions.RemoveEmptyEntries).Select((s, i) =>
                            new CreateUpdateProductAttributeDto
                            {
                                DisplayName = s.Trim(),
                                ProductAttributeOptions = new List<CreateUpdateProductAttributeOptionDto>(
                                    x.ProductAttributeOptionNames.SplitToLines(StringSplitOptions.RemoveEmptyEntries)[i]
                                        .Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o =>
                                            new CreateUpdateProductAttributeOptionDto {DisplayName = o.Trim()}))
                            })));
            CreateMap<ProductDetailDto, CreateEditProductDetailViewModel>()
                .Ignore(model => model.StoreId);
            CreateMap<CreateEditProductDetailViewModel, CreateUpdateProductDetailDto>();
            CreateMap<ProductAttributeDto, CreateEditProductAttributeViewModel>();
            CreateMap<CreateEditProductAttributeViewModel, CreateUpdateProductAttributeDto>();
            CreateMap<CreateProductSkuViewModel, CreateProductSkuDto>()
                .ForSourceMember(model => model.Inventory, opt => opt.DoNotValidate())
                .Ignore(dto => dto.AttributeOptionIds);
            CreateMap<EditProductSkuViewModel, UpdateProductSkuDto>();
            CreateMap<ProductSkuDto, EditProductSkuViewModel>()
                .ForSourceMember(dto => dto.AttributeOptionIds, opt => opt.DoNotValidate())
                .ForSourceMember(dto => dto.Inventory, opt => opt.DoNotValidate())
                .ForSourceMember(dto => dto.Sold, opt => opt.DoNotValidate());
            CreateMap<ProductAttributeOptionDto, CreateEditProductAttributeOptionViewModel>();
            CreateMap<CreateEditProductAttributeOptionViewModel, CreateUpdateProductAttributeOptionDto>();
            CreateMap<CategoryDto, CreateUpdateCategoryDto>();
        }
    }
}
