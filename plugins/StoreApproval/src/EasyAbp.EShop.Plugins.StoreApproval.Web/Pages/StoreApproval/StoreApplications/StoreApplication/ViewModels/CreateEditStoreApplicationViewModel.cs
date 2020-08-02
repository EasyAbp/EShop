using System;
using System.ComponentModel.DataAnnotations;
using EasyAbp.EShop.Plugins.StoreApproval.StoreApplications;

namespace EasyAbp.EShop.Plugins.StoreApproval.Web.Pages.StoreApproval.StoreApplications.StoreApplication.ViewModels
{
    public class CreateStoreApplicationViewModel : EditStoreApplicationViewModel
    {
        [Display(Name = "StoreApplicationApplicantId")]
        public Guid ApplicantId { get; set; }
    }

    public class EditStoreApplicationViewModel
    {
        [Display(Name = "StoreApplicationApplicantId")]
        public Guid ApplicantId { get; set; }

        [Display(Name = "StoreApplicationStoreName")]
        public string StoreName { get; set; }

        [Display(Name = "StoreApplicationBusinessCategory")]
        public string BusinessCategory { get; set; }

        [Display(Name = "StoreApplicationAddress")]
        public string Address { get; set; }

        [Display(Name = "StoreApplicationUnifiedCreditCode")]
        public string UnifiedCreditCode { get; set; }

        [Display(Name = "StoreApplicationHouseNumber")]
        public string HouseNumber { get; set; }

        [Display(Name = "StoreApplicationBusinessLicenseImage")]
        public string BusinessLicenseImage { get; set; }

        [Display(Name = "StoreApplicationName")]
        public string Name { get; set; }

        [Display(Name = "StoreApplicationIdNumber")]
        public string IdNumber { get; set; }

        [Display(Name = "StoreApplicationIdCardFrontImage")]
        public string IdCardFrontImage { get; set; }

        [Display(Name = "StoreApplicationIdCardBackImage")]
        public string IdCardBackImage { get; set; }

        [Display(Name = "StoreApplicationStoreImage")]
        public string StoreImage { get; set; }

        [Display(Name = "StoreApplicationNote")]
        public string Note { get; set; }
    }
}