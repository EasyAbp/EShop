using System.Collections.Generic;
using Volo.Abp.Domain.Values;

namespace EasyAbp.EShop.Inventory.Inventories
{
    public class Address : ValueObject
    {
        /// <summary>
        /// 街道地址
        /// </summary>
        public virtual string Street { get; set; }

        /// <summary>
        /// 区/县
        /// </summary>
        public virtual string Area { get; set; }

        /// <summary>
        /// 市
        /// </summary>
        public virtual string City { get; set; }

        /// <summary>
        /// 省
        /// </summary>
        public virtual string Province { get; set; }

        public Address()
        {
        }

        public Address(string province, string city, string area, string street)
        {
            Province = province;
            City = city;
            Area = area;
            Street = street;
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Street;
            yield return City;
            yield return Area;
            yield return Province;
        }

        public override string ToString()
        {
            return $"{Province}{City}{Area}{Street}";
        }
    }
}