using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Authorization;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Orders.Orders
{
    [Authorize]
    public class OrderAppService : MultiStoreCrudAppService<Order, OrderDto, Guid, GetOrderListDto, CreateOrderDto>,
        IOrderAppService
    {
        protected override string GetPolicyName { get; set; } = OrdersPermissions.Orders.Manage;
        protected override string GetListPolicyName { get; set; } = OrdersPermissions.Orders.Manage;
        protected override string CrossStorePolicyName { get; set; } = OrdersPermissions.Orders.CrossStore;

        private readonly INewOrderGenerator _newOrderGenerator;
        private readonly IProductAppService _productAppService;
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _repository;

        public OrderAppService(
            INewOrderGenerator newOrderGenerator,
            IProductAppService productAppService,
            IOrderManager orderManager,
            IOrderRepository repository) : base(repository)
        {
            _newOrderGenerator = newOrderGenerator;
            _productAppService = productAppService;
            _orderManager = orderManager;
            _repository = repository;
        }

        protected override async Task<IQueryable<Order>> CreateFilteredQueryAsync(GetOrderListDto input)
        {
            var query = await _repository.WithDetailsAsync();

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

        [Authorize]
        public override async Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input)
        {
            if (input.CustomerUserId != CurrentUser.GetId())
            {
                await CheckMultiStorePolicyAsync(input.StoreId, GetListPolicyName);
            }

            return await base.GetListAsync(input);
        }

        public override async Task<OrderDto> GetAsync(Guid id)
        {
            var order = await GetEntityByIdAsync(id);

            if (order.CustomerUserId != CurrentUser.GetId())
            {
                await CheckMultiStorePolicyAsync(order.StoreId, GetPolicyName);
            }

            return await MapToGetOutputDtoAsync(order);
        }

        public override async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            // Todo: Check if the store is open.

            var productDict = await GetProductDictionaryAsync(input.OrderLines.Select(dto => dto.ProductId).ToList());

            await AuthorizationService.CheckAsync(
                new OrderCreationResource
                {
                    Input = input,
                    ProductDictionary = productDict
                },
                new OrderOperationAuthorizationRequirement(OrderOperation.Creation)
            );

            var order = await _newOrderGenerator.GenerateAsync(CurrentUser.GetId(), input, productDict);

            await DiscountOrderAsync(order, productDict);

            await Repository.InsertAsync(order, autoSave: true);

            return await MapToGetOutputDtoAsync(order);
        }
        
        protected virtual async Task DiscountOrderAsync(Order order, Dictionary<Guid, ProductDto> productDict)
        {
            foreach (var provider in LazyServiceProvider.LazyGetService<IEnumerable<IOrderDiscountProvider>>())
            {
                await provider.DiscountAsync(order, productDict);
            }
        }

        protected virtual async Task<Dictionary<Guid, ProductDto>> GetProductDictionaryAsync(
            IEnumerable<Guid> productIds)
        {
            var dict = new Dictionary<Guid, ProductDto>();

            foreach (var productId in productIds.Distinct().ToList())
            {
                dict.Add(productId, await _productAppService.GetAsync(productId));
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
                await CheckMultiStorePolicyAsync(order.StoreId, OrdersPermissions.Orders.Manage);
            }

            return await MapToGetOutputDtoAsync(order);
        }

        [Authorize(OrdersPermissions.Orders.Complete)]
        public virtual async Task<OrderDto> CompleteAsync(Guid id)
        {
            var order = await GetEntityByIdAsync(id);

            if (order.CustomerUserId != CurrentUser.GetId())
            {
                await CheckMultiStorePolicyAsync(order.StoreId, OrdersPermissions.Orders.Manage);
            }

            order = await _orderManager.CompleteAsync(order);

            return await MapToGetOutputDtoAsync(order);
        }
        
        public virtual async Task<OrderDto> CancelAsync(Guid id, CancelOrderInput input)
        {
            var order = await GetEntityByIdAsync(id);
            
            await AuthorizationService.CheckAsync(
                order,
                new OrderOperationAuthorizationRequirement(OrderOperation.Cancellation)
            );

            order = await _orderManager.CancelAsync(order, input.CancellationReason);

            return await MapToGetOutputDtoAsync(order);
        }
    }
}