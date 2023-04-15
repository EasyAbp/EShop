using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products;

public class ProductDiscountResolver : IProductDiscountResolver, ITransientDependency
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

    public virtual async Task<DiscountForProductModels> ResolveAsync(IProduct product, IProductSku productSku,
        decimal priceFromPriceProvider, DateTime now)
    {
        var context = new ProductDiscountContext(now, product, productSku, priceFromPriceProvider);

        foreach (var provider in LazyServiceProvider.LazyGetService<IEnumerable<IProductDiscountProvider>>()
                     .OrderBy(x => x.EffectOrder))
        {
            await provider.DiscountAsync(context);
        }

        if (context.CandidateProductDiscounts.IsNullOrEmpty())
        {
            return new DiscountForProductModels(null, context.OrderDiscountPreviews);
        }

        var electionModel =
            new ProductDiscountElectionModel(context.Product, context.ProductSku, context.PriceFromPriceProvider);

        electionModel.TryEnqueue(new CandidateProductDiscounts(context.CandidateProductDiscounts));

        while (!electionModel.Done)
        {
            await EvolveAsync(electionModel, now);
        }

        return new DiscountForProductModels(electionModel.GetBestScheme().Discounts, context.OrderDiscountPreviews);
    }

    protected virtual Task EvolveAsync(ProductDiscountElectionModel electionModel, DateTime now)
    {
        if (electionModel.Done)
        {
            return Task.CompletedTask;
        }

        var candidateDiscounts = electionModel.Dequeue();

        var usedEffectGroup = new HashSet<string>();
        var usedDiscountNameKeyPairs = new HashSet<(string, string)>();

        var currentPrice = electionModel.PriceFromPriceProvider;

        var productDiscountInfoModels = new List<ProductDiscountInfoModel>();

        foreach (var (index, candidate) in candidateDiscounts.Items)
        {
            var discount = new ProductDiscountInfoModel(candidate, 0m, false);
            productDiscountInfoModels.Add(discount);

            if (candidate.FromTime.HasValue && now < candidate.FromTime ||
                candidate.ToTime.HasValue && now > candidate.ToTime)
            {
                continue;
            }

            if (!usedDiscountNameKeyPairs.Add(new ValueTuple<string, string>(candidate.Name, candidate.Key)))
            {
                continue;
            }

            if (!candidate.EffectGroup.IsNullOrEmpty() && !usedEffectGroup.Add(candidate.EffectGroup))
            {
                continue;
            }

            if (candidateDiscounts.Items.Values.Any(x => x.EffectGroup == candidate.EffectGroup && x != candidate))
            {
                // remove the current discount and try again to find a better scheme
                var newCandidateDiscounts = (CandidateProductDiscounts)candidateDiscounts.Clone();
                newCandidateDiscounts.Items.Remove(index);
                electionModel.TryEnqueue(newCandidateDiscounts);
            }

            discount.InEffect = true;

            discount.DiscountedAmount =
                candidate.CalculateDiscountAmount(currentPrice, electionModel.ProductSku.Currency);

            currentPrice -= discount.DiscountedAmount;
        }

        electionModel.Schemes.Add(new ProductDiscountsSchemeModel(productDiscountInfoModels));
        return Task.CompletedTask;
    }
}