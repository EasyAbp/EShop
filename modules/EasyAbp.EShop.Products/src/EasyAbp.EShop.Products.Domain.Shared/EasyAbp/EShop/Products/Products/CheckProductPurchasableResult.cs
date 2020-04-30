﻿namespace EasyAbp.EShop.Products.Products
{
    public class CheckProductPurchasableResult
    {
        public bool IsPurchasable { get; set; }
        
        public string Reason { get; set; }

        public CheckProductPurchasableResult(
            bool isPurchasable,
            string reason = null)
        {
            IsPurchasable = isPurchasable;
            Reason = reason;
        }
    }
}