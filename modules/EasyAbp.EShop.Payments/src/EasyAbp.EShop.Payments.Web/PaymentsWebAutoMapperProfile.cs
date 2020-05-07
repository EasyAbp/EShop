using EasyAbp.EShop.Payments.Payments.Dtos;
using EasyAbp.EShop.Payments.Refunds.Dtos;
using AutoMapper;

namespace EasyAbp.EShop.Payments.Web
{
    public class PaymentsWebAutoMapperProfile : Profile
    {
        public PaymentsWebAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<PaymentDto, CreatePaymentDto>();
            CreateMap<RefundDto, CreateRefundDto>();
            CreateMap<PaymentItemDto, CreatePaymentItemDto>();
        }
    }
}
