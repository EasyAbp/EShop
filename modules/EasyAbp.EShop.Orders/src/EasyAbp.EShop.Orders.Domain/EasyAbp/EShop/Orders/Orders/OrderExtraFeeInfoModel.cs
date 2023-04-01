using JetBrains.Annotations;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderExtraFeeInfoModel
    {
        [NotNull]
        public string Name { get; set; }

        [CanBeNull]
        public string Key { get; set; }

        [CanBeNull]
        public string DisplayName { get; set; }

        public decimal Fee { get; set; }

        public OrderExtraFeeInfoModel(
            [NotNull] string name,
            [CanBeNull] string key,
            [CanBeNull] string displayName,
            decimal fee)
        {
            Name = name;
            DisplayName = displayName;
            Key = key ?? string.Empty;
            Fee = fee;
        }
    }
}