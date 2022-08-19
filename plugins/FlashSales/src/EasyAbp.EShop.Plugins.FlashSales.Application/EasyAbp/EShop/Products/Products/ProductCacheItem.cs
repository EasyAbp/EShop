using System;
using EasyAbp.EShop.Products.Products.Dtos;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Products.Products;

public class ProductCacheItem : ProductDto, IMultiTenant
{
    public Guid? TenantId { get; set; }
}
