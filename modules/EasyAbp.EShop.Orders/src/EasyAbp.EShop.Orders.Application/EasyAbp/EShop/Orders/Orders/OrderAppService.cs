using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Authorization;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Orders.Orders
{
    [Authorize]
    public class OrderAppService : CrudAppService<Order, OrderDto, Guid, GetOrderListDto, CreateOrderDto>,
        IOrderAppService
    {
        protected override string CreatePolicyName { get; set; } = OrdersPermissions.Orders.Create;
        protected override string GetPolicyName { get; set; } = OrdersPermissions.Orders.Default;
        protected override string GetListPolicyName { get; set; } = OrdersPermissions.Orders.Default;
        
        private readonly INewOrderGenerator _newOrderGenerator;
        private readonly IProductAppService _productAppService;
        private readonly IPurchasableCheckManager _purchasableCheckManager;
        private readonly IOrderDiscountManager _orderDiscountManager;
        private readonly IOrderRepository _repository;

        public OrderAppService(
            INewOrderGenerator newOrderGenerator,
            IProductAppService productAppService,
            IPurchasableCheckManager purchasableCheckManager,
            IOrderDiscountManager orderDiscountManager,
            IOrderRepository repository) : base(repository)
        {
            _newOrderGenerator = newOrderGenerator;
            _productAppService = productAppService;
            _purchasableCheckManager = purchasableCheckManager;
            _orderDiscountManager = orderDiscountManager;
            _repository = repository;
        }

        protected override IQueryable<Order> CreateFilteredQuery(GetOrderListDto input)
        {
            var query = base.CreateFilteredQuery(input);

            if (input.StoreId.HasValue)
            {
                query = query.Where(x => x.StoreId == input.StoreId.Value);
            }

            if (input.CustomerUserId.HasValue)
            {
                query = query.Where(x => x.CustomerUserId == input.CustomerUserId.Value);
            }

            return query;
        }

        public override async Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input)
        {
            if (input.CustomerUserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(OrdersPermissions.Orders.Manage);

                if (input.StoreId.HasValue)
                {
                    // Todo: Check if current user is an admin of the store.
                }
                else
                {
                    await AuthorizationService.CheckAsync(OrdersPermissions.Orders.CrossStore);
                }
            }

            return await base.GetListAsync(input);
        }

        public override async Task<OrderDto> GetAsync(Guid id)
        {
            await CheckGetPolicyAsync();

            var order = await GetEntityByIdAsync(id);

            if (order.CustomerUserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(OrdersPermissions.Orders.Manage);

                // Todo: Check if current user is an admin of the store.
            }
            
            return MapToGetOutputDto(order);
        }

        public override async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            await CheckCreatePolicyAsync();

            // Todo: Check if the store is open.

            var productDict = await GetProductDictionaryAsync(input.OrderLines.Select(dto => dto.ProductId).ToList(),
                input.StoreId);

            var orderExtraProperties = new Dictionary<string, object>();

            await _purchasableCheckManager.CheckAsync(input, productDict, orderExtraProperties);
            
            var order = await _newOrderGenerator.GenerateAsync(input, productDict, orderExtraProperties);

            await _orderDiscountManager.DiscountAsync(order, input.ExtraProperties);

            await Repository.InsertAsync(order, autoSave: true);

            return MapToGetOutputDto(order);
        }

        protected virtual async Task<Dictionary<Guid, ProductDto>> GetProductDictionaryAsync(
            IEnumerable<Guid> productIds, Guid storeId)
        {
            var dict = new Dictionary<Guid, ProductDto>();

            foreach (var productId in productIds)
            {
                dict.Add(productId, await _productAppService.GetAsync(productId, storeId));
            }

            return dict;
        }

        [RemoteService(false)]
        public override Task<OrderDto> UpdateAsync(Guid id, CreateOrderDto input)
        {
            throw new NotSupportedException();
        }
        
        [RemoteService(false)]
        public override Task DeleteAsync(Guid id)
        {
            throw new NotSupportedException();
        }

        public virtual async Task<OrderDto> GetByOrderNumberAsync(string orderNumber)
        {
            await CheckGetPolicyAsync();

            var order = await _repository.GetAsync(x => x.OrderNumber == orderNumber);

            if (order.CustomerUserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckAsync(OrdersPermissions.Orders.Manage);

                // Todo: Check if current user is an admin of the store.
            }
            
            return MapToGetOutputDto(order);
        }
    }
}