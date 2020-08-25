using System;

namespace EasyAbp.EShop.Orders.Orders.Dtos
{
    [Serializable]
    public class CancelOrderInput
    {
        public string CancellationReason { get; set; }
    }
}