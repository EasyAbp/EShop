using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountResolver : IOrderDiscountResolver, ITransientDependency
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

    protected IOrderDiscountDistributor OrderDiscountDistributor =>
        LazyServiceProvider.LazyGetRequiredService<IOrderDiscountDistributor>();

    public virtual async Task<List<OrderDiscountDistributionModel>> ResolveAsync(Order order,
        Dictionary<Guid, IProduct> productDict)
    {
        var context = new OrderDiscountContext(order.CreationTime, order, productDict);

        foreach (var provider in LazyServiceProvider.LazyGetService<IEnumerable<IOrderDiscountProvider>>()
                     .OrderBy(x => x.EffectOrder))
        {
            await provider.DiscountAsync(context);
        }

        if (context.CandidateDiscounts.IsNullOrEmpty())
        {
            return new List<OrderDiscountDistributionModel>();
        }

        var electionModel = new OrderDiscountElectionModel(context.Order, context.ProductDict);
        electionModel.TryEnqueue(new CandidateOrderDiscounts(context.CandidateDiscounts));

        while (!electionModel.Done)
        {
            await EvolveAsync(electionModel);
        }

        return electionModel.GetBestScheme().Distributions;
    }

    protected virtual async Task EvolveAsync(OrderDiscountElectionModel electionModel)
    {
        if (electionModel.Done)
        {
            return;
        }

        var order = electionModel.Order;
        var candidateDiscounts = electionModel.Dequeue();

        // Make sure that each OrderLine can only be affected by one discount with the same EffectGroup.
        var affectedOrderLineIdsInEffectGroup = new Dictionary<string, List<Guid>>();
        var usedDiscountNameKeyPairs = new HashSet<(string, string)>();

        var currentTotalPrices =
            new Dictionary<IOrderLine, decimal>(order.OrderLines.ToDictionary(x => x, x => x.TotalPrice));

        var distributionModels = new List<OrderDiscountDistributionModel>();

        foreach (var (index, discount) in candidateDiscounts.Items)
        {
            if (!usedDiscountNameKeyPairs.Add(new ValueTuple<string, string>(discount.Name, discount.Key)))
            {
                continue;
            }

            if (!discount.EffectGroup.IsNullOrEmpty())
            {
                var affectedOrderLineIds =
                    affectedOrderLineIdsInEffectGroup.GetOrAdd(discount.EffectGroup!, () => new List<Guid>());

                if (discount.AffectedOrderLineIds.Any(x => affectedOrderLineIds.Contains(x)))
                {
                    continue;
                }

                affectedOrderLineIdsInEffectGroup[discount.EffectGroup!].AddRange(discount.AffectedOrderLineIds);
            }

            if (candidateDiscounts.Items.Values.Any(x => x.EffectGroup == discount.EffectGroup && x != discount))
            {
                // remove the current discount and try again to find a better scheme
                var newCandidateDiscounts = (CandidateOrderDiscounts)candidateDiscounts.Clone();
                newCandidateDiscounts.Items.Remove(index);
                electionModel.TryEnqueue(newCandidateDiscounts);
            }

            var distributionResult =
                await OrderDiscountDistributor.DistributeAsync(order, currentTotalPrices, discount);

            distributionModels.Add(new OrderDiscountDistributionModel(discount, currentTotalPrices,
                distributionResult.Distributions));
        }

        electionModel.Schemes.Add(new OrderDiscountsSchemeModel(distributionModels));
    }
}