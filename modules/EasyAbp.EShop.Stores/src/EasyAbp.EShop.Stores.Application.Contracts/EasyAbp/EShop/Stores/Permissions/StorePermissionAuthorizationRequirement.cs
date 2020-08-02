using System;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Stores.Permissions
{
    public class StorePermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        internal StorePermissionAuthorizationRequirement(Guid storeId, string policyName = null)
        {
            StoreId = storeId;
            PolicyName = policyName;
        }

        public Guid StoreId { get; }

        public string PolicyName { get; }
    }
}
