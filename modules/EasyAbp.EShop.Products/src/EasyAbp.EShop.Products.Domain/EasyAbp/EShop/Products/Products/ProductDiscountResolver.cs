using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.Products;

public class ProductDiscountResolver : IProductDiscountResolver, ITransientDependency
{
    public IAbpLazyServiceProvider LazyServiceProvider { get; set; }

    public virtual async Task ResolveAsync(GetProductsRealTimePriceContext context)
    {
        foreach (var provider in LazyServiceProvider.LazyGetService<IEnumerable<IProductDiscountProvider>>()
                     .OrderBy(x => x.EffectOrder))
        {
            await provider.DiscountAsync(context);
        }

        foreach (var model in context.Models.Values)
        {
            var product = context.Products[model.ProductId];
            var productSku = product.GetSkuById(model.ProductSkuId);

            var electionModel =
                new ProductDiscountElectionModel(context.Now, product, productSku, model.PriceWithoutDiscount);

            electionModel.TryEnqueue(new CandidateProductDiscounts(model.CandidateProductDiscounts));

            while (!electionModel.Done)
            {
                await EvolveAsync(electionModel);
            }

            model.ProductDiscounts.AddRange(electionModel.GetBestScheme().Discounts);
        }
    }

    protected virtual Task EvolveAsync(ProductDiscountElectionModel electionModel)
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

            if (candidate.FromTime.HasValue && electionModel.Now < candidate.FromTime ||
                candidate.ToTime.HasValue && electionModel.Now > candidate.ToTime)
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