using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class ProductCodeDuplicatedException : BusinessException
    {
        public ProductCodeDuplicatedException(string code) : base(
            message: $"Product code {code} is duplicate.")
        {
        }
    }
}