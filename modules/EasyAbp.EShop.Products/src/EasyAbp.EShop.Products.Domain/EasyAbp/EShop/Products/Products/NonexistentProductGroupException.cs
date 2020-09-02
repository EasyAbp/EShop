using System;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products
{
    public class NonexistentProductGroupException : BusinessException
    {
        public NonexistentProductGroupException(string productGroupName) : base(
            "NonexistentProductGroup",
            $"The specified product group ({productGroupName}) is nonexistent.")
        {
        }
    }
}