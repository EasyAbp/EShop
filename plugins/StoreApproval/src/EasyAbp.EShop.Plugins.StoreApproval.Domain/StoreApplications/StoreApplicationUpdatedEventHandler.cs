using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications
{
    public class StoreApplicationUpdatedEventHandler
        : ILocalEventHandler<EntityUpdatedEventData<StoreApplication>>,
            ITransientDependency
    {
        private readonly IStoreManager _storeManager;
        private readonly IGuidGenerator _guidGenerator;
        private readonly ICurrentTenant _currentTenant;
        private readonly IdentityUserManager _userManager;

        public StoreApplicationUpdatedEventHandler(IStoreManager storeManager,
            IGuidGenerator guidGenerator,
            ICurrentTenant currentTenant,
            IdentityUserManager userManager)
        {
            _storeManager = storeManager;
            _guidGenerator = guidGenerator;
            _currentTenant = currentTenant;
            _userManager = userManager;
        }

        public async Task HandleEventAsync(
            EntityUpdatedEventData<StoreApplication> eventData)
        {
            switch (eventData.Entity.ApprovalStatus)
            {
                case ApprovalStatusType.Preparing:
                case ApprovalStatusType.ReadyForReview:
                case ApprovalStatusType.Rejected:
                    break;
                case ApprovalStatusType.Approved:
                    await CreateStore(eventData.Entity.StoreName, eventData.Entity.ApplicantId);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task CreateStore(string storeName, Guid userId)
        {
            const string merchant = nameof(merchant);

            var user = await _userManager.GetByIdAsync(userId);

            var store = await _storeManager.CreateAsync(new Store(
                _guidGenerator.Create(),
                _currentTenant.Id,
                storeName), new []{ userId });

            (await _userManager.SetRolesAsync(user, new []{ merchant })).CheckErrors();
        }
    }
}
