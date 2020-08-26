using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.Authorization;
using EasyAbp.EShop.Orders.Orders.Dtos;
using EasyAbp.EShop.Products.Products;
using EasyAbp.EShop.Products.Products.Dtos;
using EasyAbp.EShop.Stores.Permissions;
using EasyAbp.EShop.Stores.Stores;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Authorization;
using Volo.Abp.Users;

namespace EasyAbp.EShop.Orders.Orders
{
    [Authorize]
    public class OrderAppService : MultiStoreCrudAppService<Order, OrderDto, Guid, GetOrderListDto, CreateOrderDto>,
        IOrderAppService
    {
        protected override string CreatePolicyName { get; set; } = OrdersPermissions.Orders.Create;
        protected override string GetPolicyName { get; set; } = OrdersPermissions.Orders.Manage;
        protected override string GetListPolicyName { get; set; } = OrdersPermissions.Orders.Manage;
        protected override string CrossStorePolicyName { get; set; } = OrdersPermissions.Orders.CrossStore;

        private readonly INewOrderGenerator _newOrderGenerator;
        private readonly IProductAppService _productAppService;
        private readonly IPurchasableChecker _purchasableChecker;
        private readonly IOrderManager _orderManager;
        private readonly IOrderRepository _repository;

        public OrderAppService(
            INewOrderGenerator newOrderGenerator,
            IProductAppService productAppService,
            IPurchasableChecker purchasableChecker,
            IOrderManager orderManager,
            IOrderRepository repository) : base(repository)
        {
            _newOrderGenerator = newOrderGenerator;
            _productAppService = productAppService;
            _purchasableChecker = purchasableChecker;
            _orderManager = orderManager;
            _repository = repository;
        }

        protected override IQueryable<Order> CreateFilteredQuery(GetOrderListDto input)
        {
            var query = _repository.WithDetails();

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
                if (input.StoreId.HasValue)
                {
                    await CheckMultiStorePolicyAsync(input.StoreId.Value, GetListPolicyName);
                }
                else
                {
                    throw new AbpAuthorizationException("Authorization failed! Given policy has not granted: " +
                                                        GetListPolicyName);
                }
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

            return MapToGetOutputDto(order);
        }

        public override async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            await CheckMultiStorePolicyAsync(input.StoreId, CreatePolicyName);

            // Todo: Check if the store is open.

            var productDict = await GetProductDictionaryAsync(input.OrderLines.Select(dto => dto.ProductId).ToList(),
                input.StoreId);

            var orderExtraProperties = new Dictionary<string, object>();

            await _purchasableChecker.CheckAsync(input, productDict, orderExtraProperties);

            var order = await _newOrderGenerator.GenerateAsync(input, productDict, orderExtraProperties);

            await _orderManager.DiscountAsync(order, input.ExtraProperties);

            await Repository.InsertAsync(order, autoSave: true);

            return MapToGetOutputDto(order);
        }

        protected virtual async Task<Dictionary<Guid, ProductDto>> GetProductDictionaryAsync(
            IEnumerable<Guid> productIds, Guid storeId)
        {
            var dict = new Dictionary<Guid, ProductDto>();

            foreach (var productId in productIds.Distinct().ToList())
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
            var order = await _repository.GetAsync(x => x.OrderNumber == orderNumber);

            if (order.CustomerUserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckStoreOwnerAsync(order.StoreId, OrdersPermissions.Orders.Manage);
            }

            return MapToGetOutputDto(order);
        }

        [Authorize(OrdersPermissions.Orders.Complete)]
        public virtual async Task<OrderDto> CompleteAsync(Guid id)
        {
            var order = await GetEntityByIdAsync(id);

            if (order.CustomerUserId != CurrentUser.GetId())
            {
                await AuthorizationService.CheckStoreOwnerAsync(order.StoreId, OrdersPermissions.Orders.Manage);
            }

            order = await _orderManager.CompleteAsync(order);

            return MapToGetOutputDto(order);
        }

        public virtual Task<OrderDto> CancelAsync(Guid id, CancelOrderInput input)
        {
            throw new NotImplementedException();
        }
    }
}