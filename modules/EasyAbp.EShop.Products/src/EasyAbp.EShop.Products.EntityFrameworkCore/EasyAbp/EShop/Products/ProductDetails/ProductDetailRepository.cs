﻿using System;
using EasyAbp.EShop.Products.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Products.ProductDetails
{
    public class ProductDetailRepository : EfCoreRepository<ProductsDbContext, ProductDetail, Guid>, IProductDetailRepository
    {
        public ProductDetailRepository(IDbContextProvider<ProductsDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}