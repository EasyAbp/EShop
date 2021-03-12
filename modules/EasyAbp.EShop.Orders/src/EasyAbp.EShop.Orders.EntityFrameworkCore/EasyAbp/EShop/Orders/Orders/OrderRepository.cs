using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EasyAbp.EShop.Orders.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace EasyAbp.EShop.Orders.Orders
{
    public class OrderRepository : EfCoreRepository<IOrdersDbContext, Order, Guid>, IOrderRepository
    {
        public OrderRepository(IDbContextProvider<IOrdersDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<Order>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync())
                .Include(x => x.OrderLines)
                .Include(x => x.OrderExtraFees);
        }

        public override Task<Order> UpdateAsync(Order entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            Check.NotNullOrWhiteSpace(entity.OrderNumber, nameof(entity.OrderNumber));
            
            return base.UpdateAsync(entity, autoSave, cancellationToken);
        }

        public override Task<Order> InsertAsync(Order entity, bool autoSave = false, CancellationToken cancellationToken = new CancellationToken())
        {
            Check.NotNullOrWhiteSpace(entity.OrderNumber, nameof(entity.OrderNumber));
            
            return base.InsertAsync(entity, autoSave, cancellationToken);
        }
    }
}