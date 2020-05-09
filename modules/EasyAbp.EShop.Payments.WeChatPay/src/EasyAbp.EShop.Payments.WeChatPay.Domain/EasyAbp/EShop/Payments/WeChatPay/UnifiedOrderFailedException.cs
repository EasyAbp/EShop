using Volo.Abp;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    public class UnifiedOrderFailedException : BusinessException
    {
        public UnifiedOrderFailedException(string returnCode, string returnMsg) : base(
            message: $"Unified order failed, return_code: {returnCode}, return_msg: {returnMsg}")
        {
        }

        public UnifiedOrderFailedException(string returnCode, string returnMsg, string errCode, string errCodeDes) :
            base(message: $"Unified order failed, return_code: {returnCode}, return_msg: {returnMsg}, err_code: {errCode}, err_code_des: {errCodeDes}")
        {
        }
    }
}