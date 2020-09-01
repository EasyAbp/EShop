using System;
using Microsoft.AspNetCore.Authorization;

namespace EasyAbp.EShop.Stores.Authorization
{
    public class StorePermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public StorePermissionAuthorizationRequirement(string policyName = null)
        {
            PolicyName = policyName;
        }

        public string PolicyName { get; }
    }
}
