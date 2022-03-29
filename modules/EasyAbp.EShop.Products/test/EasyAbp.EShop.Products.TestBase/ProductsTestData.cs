using System;

namespace EasyAbp.EShop.Products
{
    public class ProductsTestData
    {
        public static Guid Store1Id { get; } = Guid.NewGuid();

        public static Guid ProductDetails1Id { get; } = Guid.NewGuid();
        
        public static Guid ProductDetails2Id { get; } = Guid.NewGuid();
        
        public static Guid Product1Id { get; } = Guid.NewGuid();
        
        public static Guid Product1Attribute1Id { get; } = Guid.NewGuid();
        
        public static Guid Product1Attribute1Option1Id { get; } = Guid.NewGuid();
        
        public static Guid Product1Attribute1Option2Id { get; } = Guid.NewGuid();
        
        public static Guid Product1Attribute1Option3Id { get; } = Guid.NewGuid();
        
        public static Guid Product1Sku1Id { get; } = Guid.NewGuid();
        
        public static Guid Product1Sku2Id { get; } = Guid.NewGuid();
        
        public static Guid Product1Sku3Id { get; } = Guid.NewGuid();
    }
}