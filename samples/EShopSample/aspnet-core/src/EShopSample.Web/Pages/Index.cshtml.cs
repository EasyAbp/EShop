using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Orders;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Stores.Stores;
using EasyAbp.EShop.Stores.Stores.Dtos;
using EasyAbp.PaymentService.Prepayment.Accounts;
using EasyAbp.PaymentService.Prepayment.Accounts.Dtos;
using EShopSample.Data;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace EShopSample.Web.Pages;

public class IndexModel : EShopSamplePageModel
{
    private readonly IOrderAppService _orderAppService;
    private readonly IStoreAppService _storeAppService;
    private readonly IProductAppService _productAppService;
    private readonly IAccountAppService _accountAppService;

    public OrderDto Order { get; set; }
    public PagedResultDto<OrderDto> OrderList { get; set; }
    public StoreDto Store { get; set; }
    public ProductDto CakeProduct { get; set; }
    public AccountDto Wallet { get; set; }

    public IndexModel(
        IOrderAppService orderAppService,
        IStoreAppService storeAppService,
        IProductAppService productAppService,
        IAccountAppService accountAppService)
    {
        _orderAppService = orderAppService;
        _storeAppService = storeAppService;
        _productAppService = productAppService;
        _accountAppService = accountAppService;
    }

    public async Task OnGetAsync()
    {
        if (CurrentUser.Id is null)
        {
            return;
        }

        OrderList = await _orderAppService.GetListAsync(new GetOrderListDto
        {
            MaxResultCount = 5,
            CustomerUserId = CurrentUser.GetId(),
            Sorting = "CreationTime DESC"
        });

        Store = await _storeAppService.GetDefaultAsync();

        CakeProduct = await _productAppService.GetByUniqueNameAsync(Store.Id, SampleDataConsts.CakeProductUniqueName);

        Order = OrderList.Items.FirstOrDefault(x => x.OrderStatus is OrderStatus.Pending && x.OrderLines.Any(ol => ol.ProductId == CakeProduct.Id));

        Wallet = (await _accountAppService.GetListAsync(new GetAccountListInput { UserId = CurrentUser.Id })).Items[0];
    }

    public string GetJsonSkuInfo()
    {
        var sb = new StringBuilder("[");

        foreach (var sku in CakeProduct.ProductSkus)
        {
            sb.Append('{');
            sb.Append("\"skus\":");
            sb.Append('{');

            foreach (var optionId in sku.AttributeOptionIds)
            {
                var option = CakeProduct.ProductAttributes.SelectMany(x => x.ProductAttributeOptions)
                    .Single(x => x.Id == optionId);

                var attribute = CakeProduct.ProductAttributes.Single(x => x.ProductAttributeOptions.Contains(option));

                sb.Append($"\"{L[attribute.DisplayName].Value}\":\"{L[option.DisplayName].Value}\"");

                if (optionId != sku.AttributeOptionIds.Last())
                {
                    sb.Append(',');
                }
            }

            sb.Append('}');

            sb.Append($",\"skuId\":\"{sku.Id}\"");
            sb.Append($",\"skuPrice\":{sku.Price}");
            sb.Append($",\"skuCurrency\":\"{sku.Currency}\"");

            sb.Append('}');

            if (sku != CakeProduct.ProductSkus.Last())
            {
                sb.Append(',');
            }
        }

        sb.Append(']');

        return sb.ToString();
    }

    public int GetSecondsToAutoCancel()
    {
        if (Order is null)
        {
            return 0;
        }

        return Order.PaymentExpiration.HasValue
            ? Convert.ToInt32((Order.PaymentExpiration.Value - Clock.Now).TotalSeconds)
            : 0;
    }

    public string GetOrderStatusColumnClass(OrderDto order)
    {
        if (order is null)
        {
            return string.Empty;
        }

        return order.OrderStatus switch
        {
            OrderStatus.Pending => "status-pending-text",
            OrderStatus.Processing => "status-processing-text",
            OrderStatus.Completed => "status-completed-text",
            OrderStatus.Canceled => "status-canceled-text",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}