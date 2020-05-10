using Volo.Abp;

namespace EasyAbp.EShop.Payments.WeChatPay
{
    public class XmlDocumentMissingRequiredElementException : BusinessException
    {
        public XmlDocumentMissingRequiredElementException(string elementTag) : base(
            message: $"XmlDocument missing required element: {elementTag}")
        {
        }
    }
}