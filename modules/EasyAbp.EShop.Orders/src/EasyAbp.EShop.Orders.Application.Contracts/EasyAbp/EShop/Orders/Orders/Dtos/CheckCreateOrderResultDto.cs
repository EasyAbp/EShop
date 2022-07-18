using System;

namespace EasyAbp.EShop.Orders.Orders.Dtos;

[Serializable]
public class CheckCreateOrderResultDto
{
    public bool CanCreate { get; set; }
    
    public string Reason { get; set; }
}