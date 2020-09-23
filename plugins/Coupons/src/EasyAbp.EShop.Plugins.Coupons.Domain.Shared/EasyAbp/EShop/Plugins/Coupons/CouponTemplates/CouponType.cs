namespace EasyAbp.EShop.Plugins.Coupons.CouponTemplates
{
    public enum CouponType
    {
        /// <summary>
        /// Discount once if the condition is met.
        /// e.g. arrange ConditionAmount = 300 and DiscountAmount = 20.
        /// If the price of a product is 300, the actual order price will be discounted by 20.
        /// </summary>
        Normal = 1,
        
        /// <summary>
        /// Discount every time the condition is met.
        /// e.g. arrange ConditionAmount = 300 and DiscountAmount = 20.
        /// If the price of a product is 300, the actual order price will be discounted by 60.
        /// </summary>
        PerMeet = 2,
        
        /// <summary>
        /// A type of coupon with custom usage rules.
        /// </summary>
        Custom = 4
    }
}