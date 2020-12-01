using JetBrains.Annotations;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderExtraFeeInfoModel
    {
        [NotNull]
        public string Name { get; set; }
        
        [CanBeNull]
        public string Key { get; set; }
        
        public decimal Fee { get; set; }

        public OrderExtraFeeInfoModel(
            [NotNull] string name,
            [CanBeNull] string key,
            decimal fee)
        {
            Name = name;
            Key = key ?? string.Empty;
            Fee = fee;
        }
    }
}