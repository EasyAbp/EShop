﻿using Volo.Abp.Reflection;

namespace EasyAbp.EShop.Plugins.Baskets.Permissions
{
    public class BasketsPermissions
    {
        public const string GroupName = "EasyAbp.EShop.Plugins.Baskets";

        public static string[] GetAll()
        {
            return ReflectionHelper.GetPublicConstantsRecursively(typeof(BasketsPermissions));
        }

        public class BasketItem
        {
            public const string Default = GroupName + ".BasketItem";
            public const string Manage = Default + ".Manage";
        }

    }
}
