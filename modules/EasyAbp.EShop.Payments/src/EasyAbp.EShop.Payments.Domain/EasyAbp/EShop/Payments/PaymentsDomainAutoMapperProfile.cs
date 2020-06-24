using AutoMapper;
using EasyAbp.EShop.Payments.Payments;
using EasyAbp.PaymentService.Payments;
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
                .Ignore(x => x.StoreId)
                .Ignore(x => x.PaymentItems);
            CreateMap<PaymentItemEto, PaymentItem>(MemberList.Source);
        }
    }
}
