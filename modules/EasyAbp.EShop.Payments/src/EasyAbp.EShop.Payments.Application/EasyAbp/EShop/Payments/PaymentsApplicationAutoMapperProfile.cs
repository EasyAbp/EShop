using EasyAbp.EShop.Payments.Payments;
using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.EShop.Payments.Refunds;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using AutoMapper;

namespace EasyAbp.EShop.Payments
{
    public class PaymentsApplicationAutoMapperProfile : Profile
    {
        public PaymentsApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Payment, PaymentDto>();
            CreateMap<CreateUpdatePaymentDto, Payment>(MemberList.Source);
            CreateMap<Refund, RefundDto>();
            CreateMap<CreateUpdateRefundDto, Refund>(MemberList.Source);
        }
    }
}
