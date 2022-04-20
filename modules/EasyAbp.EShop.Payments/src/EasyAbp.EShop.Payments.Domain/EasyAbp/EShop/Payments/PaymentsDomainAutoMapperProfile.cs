using AutoMapper;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.PaymentService.Payments;
using EasyAbp.PaymentService.Refunds;
using Volo.Abp.AutoMapper;

namespace EasyAbp.EShop.Payments
{
    public class PaymentsDomainAutoMapperProfile : Profile
    {
        public PaymentsDomainAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */

            CreateMap<PaymentEto, Payment>(MemberList.Source)
                .Ignore(x => x.PaymentItems);
            CreateMap<PaymentItemEto, PaymentItem>(MemberList.Source)
                .Ignore(x => x.StoreId);
            
            CreateMap<RefundEto, Refund>(MemberList.Source)
                .Ignore(x => x.RefundItems);
            CreateMap<RefundItemEto, RefundItem>(MemberList.Source)
                .Ignore(x => x.StoreId)
                .Ignore(x => x.OrderId);
            
            CreateMap<Payment, EShopPaymentEto>();
            CreateMap<PaymentItem, EShopPaymentItemEto>();
            CreateMap<Refund, EShopRefundEto>();
            CreateMap<RefundItem, EShopRefundItemEto>();
            CreateMap<RefundItemOrderLine, RefundItemOrderLineEto>();
            CreateMap<RefundItemOrderExtraFee, RefundItemOrderExtraFeeEto>();
        }
    }
}
