using System;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.EShop.Plugins.StoreApproval.StoreApplications
{
    public class StoreApplication : FullAuditedAggregateRoot<Guid>, IMultiTenant
    {
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid ApplicantId { get; protected set; }

        public virtual ApprovalStatusType ApprovalStatus { get; protected set; }

        [NotNull]
        public virtual string StoreName { get; protected set; }

        [NotNull]
        public virtual string BusinessCategory { get; protected set; }

        [NotNull]
        public virtual string Address { get; protected set; }

        [NotNull]
        public virtual string UnifiedCreditCode { get; protected set; }

        [CanBeNull]
        public virtual string HouseNumber { get; protected set; }

        [NotNull]
        public virtual string BusinessLicenseImage { get; protected set; }

        [NotNull]
        public virtual string Name { get; protected set; }

        [NotNull]
        public virtual string IdNumber { get; protected set; }

        [NotNull]
        public virtual string IdCardFrontImage { get; protected set; }

        [NotNull]
        public virtual string IdCardBackImage { get; protected set; }

        [NotNull]
        public virtual string StoreImage { get; protected set; }

        [CanBeNull]
        public virtual string Note { get; protected set; }

        protected StoreApplication()
        {
        }

        public StoreApplication(Guid id, Guid? tenantId, Guid applicantId, ApprovalStatusType approvalStatus, string storeName, string businessCategory, string address, string unifiedCreditCode, string houseNumber, string businessLicenseImage, string name, string idNumber, string idCardFrontImage, string idCardBackImage, string storeImage, string note) : base(id)
        {
            TenantId = tenantId;
            ApplicantId = applicantId;
            ApprovalStatus = approvalStatus;
            StoreName = storeName;
            BusinessCategory = businessCategory;
            Address = address;
            UnifiedCreditCode = unifiedCreditCode;
            HouseNumber = houseNumber;
            BusinessLicenseImage = businessLicenseImage;
            Name = name;
            IdNumber = idNumber;
            IdCardFrontImage = idCardFrontImage;
            IdCardBackImage = idCardBackImage;
            StoreImage = storeImage;
            Note = note;
        }

        public void Submit()
        {
            switch (ApprovalStatus)
            {
                case ApprovalStatusType.Preparing:
                case ApprovalStatusType.Rejected:
                    ApprovalStatus = ApprovalStatusType.ReadyForReview;
                    //TODO event
                    break;
                case ApprovalStatusType.ReadyForReview:
                    throw new StoreApplicationAlreadySubmittedException();
                case ApprovalStatusType.Approved:
                    throw new StoreApplicationAlreadyApprovedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Approve()
        {
            switch (ApprovalStatus)
            {
                case ApprovalStatusType.ReadyForReview:
                    ApprovalStatus = ApprovalStatusType.Approved;
                    //TODO event
                    break;
                case ApprovalStatusType.Preparing:
                case ApprovalStatusType.Rejected:
                    throw new StoreApplicationNotSubmittedException();
                case ApprovalStatusType.Approved:
                    throw new StoreApplicationAlreadyApprovedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Reject()
        {
            switch (ApprovalStatus)
            {
                case ApprovalStatusType.ReadyForReview:
                    ApprovalStatus = ApprovalStatusType.Rejected;
                    //TODO event
                    break;
                case ApprovalStatusType.Preparing:
                case ApprovalStatusType.Rejected:
                    throw new StoreApplicationNotSubmittedException();
                case ApprovalStatusType.Approved:
                    throw new StoreApplicationAlreadyApprovedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
