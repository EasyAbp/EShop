using System;
using EasyAbp.EShop.Plugins.Coupons.Permissions;
using EasyAbp.EShop.Plugins.Coupons.Coupons.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.EShop.Plugins.Coupons.Coupons
{
    public class CouponAppService : CrudAppService<Coupon, CouponDto, Guid, PagedAndSortedResultRequestDto, CreateUpdateCouponDto, CreateUpdateCouponDto>,
        ICouponAppService
    {
        protected override string GetPolicyName { get; set; } = CouponsPermissions.Coupon.Default;
        protected override string GetListPolicyName { get; set; } = CouponsPermissions.Coupon.Default;
        protected override string CreatePolicyName { get; set; } = CouponsPermissions.Coupon.Create;
        protected override string UpdatePolicyName { get; set; } = CouponsPermissions.Coupon.Update;
        protected override string DeletePolicyName { get; set; } = CouponsPermissions.Coupon.Delete;

        private readonly ICouponRepository _repository;
        
        public CouponAppService(ICouponRepository repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
