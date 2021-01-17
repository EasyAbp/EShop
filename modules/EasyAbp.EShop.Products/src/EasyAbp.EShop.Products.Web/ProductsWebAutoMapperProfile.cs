using System;
using System.Collections.Generic;
using System.Linq;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Products.Categories.Dtos;
using AutoMapper;
using EasyAbp.EShop.Products.ProductDetails.Dtos;
using EasyAbp.EShop.Products.Web.Pages.EShop.Products.Categories.Category.ViewModels;
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
            CreateMap<ProductDto, EditProductViewModel>()
                .Ignore(model => model.CategoryIds)
                .Ignore(model => model.ProductDetail)
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
            CreateMap<CreateProductViewModel, CreateUpdateProductDto>()
                .Ignore(dto => dto.ExtraProperties)
                .Ignore(dto => dto.ProductDetailId)
                .ForSourceMember(model => model.ProductDetail, opt => opt.DoNotValidate())
                .ForMember(dest => dest.ProductAttributes,
                    opt => opt.MapFrom(x =>
                        x.ProductAttributeNames.Split(",", StringSplitOptions.RemoveEmptyEntries).Select((s, i) =>
                            new CreateUpdateProductAttributeDto
                            {
                                DisplayName = s,
                                ProductAttributeOptions = new List<CreateUpdateProductAttributeOptionDto>(
                                    x.ProductAttributeOptionNames.SplitToLines(StringSplitOptions.RemoveEmptyEntries)[i]
                                        .Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o =>
                                            new CreateUpdateProductAttributeOptionDto {DisplayName = o}))
                            })));
            CreateMap<EditProductViewModel, CreateUpdateProductDto>()
                .Ignore(dto => dto.StoreId)
                .Ignore(dto => dto.ExtraProperties)
                .Ignore(dto => dto.ProductDetailId)
                .ForSourceMember(model => model.ProductDetail, opt => opt.DoNotValidate())
                .ForMember(dest => dest.ProductAttributes,
                    opt => opt.MapFrom(x =>
                        x.ProductAttributeNames.Split(",", StringSplitOptions.RemoveEmptyEntries).Select((s, i) =>
                            new CreateUpdateProductAttributeDto
                            {
                                DisplayName = s,
                                ProductAttributeOptions = new List<CreateUpdateProductAttributeOptionDto>(
                                    x.ProductAttributeOptionNames.SplitToLines(StringSplitOptions.RemoveEmptyEntries)[i]
                                        .Split(",", StringSplitOptions.RemoveEmptyEntries).Select(o =>
                                            new CreateUpdateProductAttributeOptionDto {DisplayName = o}))
                            })));
            CreateMap<ProductDetailDto, CreateProductDetailViewModel>();
            CreateMap<CreateProductDetailViewModel, CreateUpdateProductDetailDto>()
                .Ignore(dto => dto.ExtraProperties);
            CreateMap<EditProductDetailViewModel, CreateUpdateProductDetailDto>()
                .Ignore(dto => dto.StoreId)
                .Ignore(dto => dto.ExtraProperties);
            CreateMap<ProductAttributeDto, CreateEditProductAttributeViewModel>();
            CreateMap<CreateEditProductAttributeViewModel, CreateUpdateProductAttributeDto>()
                .Ignore(dto => dto.ExtraProperties);
            CreateMap<CreateProductSkuViewModel, CreateProductSkuDto>()
                .Ignore(dto => dto.ExtraProperties)
                .ForSourceMember(model => model.Inventory, opt => opt.DoNotValidate())
                .Ignore(dto => dto.AttributeOptionIds);
            CreateMap<EditProductSkuViewModel, UpdateProductSkuDto>()
                .Ignore(dto => dto.ExtraProperties);
            CreateMap<ProductSkuDto, EditProductSkuViewModel>()
                .ForSourceMember(dto => dto.AttributeOptionIds, opt => opt.DoNotValidate())
                .ForSourceMember(dto => dto.Inventory, opt => opt.DoNotValidate())
                .ForSourceMember(dto => dto.Sold, opt => opt.DoNotValidate());
            CreateMap<ProductAttributeOptionDto, CreateEditProductAttributeOptionViewModel>();
            CreateMap<CreateEditProductAttributeOptionViewModel, CreateUpdateProductAttributeOptionDto>()
                .Ignore(dto => dto.ExtraProperties);
            CreateMap<CategoryDto, CreateEditCategoryViewModel>(MemberList.Destination);
            CreateMap<CreateEditCategoryViewModel, CreateUpdateCategoryDto>()
                .Ignore(dto => dto.ExtraProperties);
        }
    }
}
