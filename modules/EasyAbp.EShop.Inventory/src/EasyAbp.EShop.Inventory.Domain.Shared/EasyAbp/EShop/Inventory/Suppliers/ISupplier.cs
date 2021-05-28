using EasyAbp.EShop.Inventory.Inventories;

namespace EasyAbp.EShop.Inventory.Suppliers
{
    /// <summary>
    /// 供应商接口
    /// </summary>
    public interface ISupplier
    {
        /// <summary>
        /// 名字
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 地址
        /// </summary>
        Address Address { get; }

        /// <summary>
        /// 描述
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 联系人
        /// </summary>
        string ContactName { get; }

        /// <summary>
        /// 联系电话
        /// </summary>
        string ContactPhoneNumber { get; }

        /// <summary>
        /// 经营范围
        /// </summary>
        string BusinessScope { get; }
    }
}