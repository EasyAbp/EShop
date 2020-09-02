using System;
using System.Reflection;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Options.ProductGroups
{
    public class ProductGroupNameAttribute : Attribute
    {
        [NotNull]
        public string Name { get; }

        public ProductGroupNameAttribute([NotNull] string name)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            Name = name;
        }

        public virtual string GetName(Type type)
        {
            return Name;
        }

        public static string GetGroupName<T>()
        {
            return GetGroupName(typeof(T));
        }

        public static string GetGroupName(Type type)
        {
            var nameAttribute = type.GetCustomAttribute<ProductGroupNameAttribute>();

            if (nameAttribute == null)
            {
                return type.FullName;
            }

            return nameAttribute.GetName(type);
        }
    }
}