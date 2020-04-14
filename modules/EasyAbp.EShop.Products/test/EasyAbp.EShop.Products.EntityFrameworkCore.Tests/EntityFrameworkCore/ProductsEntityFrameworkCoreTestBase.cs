﻿namespace EasyAbp.EShop.Products.EntityFrameworkCore
{
    /* This class can be used as a base class for EF Core integration tests,
     * while SampleRepository_Tests uses a different approach.
     */
    public abstract class ProductsEntityFrameworkCoreTestBase : ProductsTestBase<EShopProductsEntityFrameworkCoreTestModule>
    {

    }
}