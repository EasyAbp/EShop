using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp;

namespace EasyAbp.EShop.Products.Products;

public class ProductDiscountElectionModel
{
    public DateTime Now { get; }

    /// <summary>
    /// The search will stop and throw an exception if the number of executions is greater than <see cref="MaxDepth"/>.
    /// <see cref="MaxDepth"/> = Math.Pow(2, <see cref="MaxCandidates"/>)
    /// </summary>
    public static int MaxCandidates { get; set; } = 10;

    /// <summary>
    /// The search will stop and throw an exception if the number of executions is greater than <see cref="MaxDepth"/>.
    /// This is to prevent a dead loop.
    /// </summary>
    private static double MaxDepth { get; set; } = Math.Pow(2, MaxCandidates);

    /// <summary>
    /// The search will stop and throw an exception if this value is greater than <see cref="MaxDepth"/>.
    /// </summary>
    public double CurrentDepth { get; private set; }

    public IProduct Product { get; }

    public IProductSku ProductSku { get; }

    public decimal PriceFromPriceProvider { get; }

    public List<ProductDiscountsSchemeModel> Schemes { get; } = new();

    public bool Done => Schemes.Any() && !CandidateDiscountsQueue.Any();

    private Queue<CandidateProductDiscounts> CandidateDiscountsQueue { get; } = new();

    private HashSet<string> UsedCombinations { get; } = new();

    public ProductDiscountElectionModel(DateTime now, IProduct product, IProductSku productSku,
        decimal priceFromPriceProvider)
    {
        Now = now;
        Product = product;
        ProductSku = productSku;
        PriceFromPriceProvider = priceFromPriceProvider;
    }

    public ProductDiscountsSchemeModel GetBestScheme()
    {
        if (!Done)
        {
            throw new AbpException("The ProductDiscountElectionModel is in an incorrect state.");
        }

        return Schemes.MaxBy(x => x.TotalDiscountAmount);
    }

    public bool TryEnqueue(CandidateProductDiscounts candidateDiscounts)
    {
        if (candidateDiscounts.Items.Count > MaxCandidates)
        {
            throw new AbpException(
                $"The ProductDiscountElectionModel cannot handle a CandidateProductDiscounts with the number of candidate discounts > {MaxCandidates}.");
        }

        CurrentDepth++;

        if (CurrentDepth > MaxDepth || CurrentDepth < 0)
        {
            throw new AbpException("The ProductDiscountElectionModel's search exceeded maximum depth.");
        }

        if (!UsedCombinations.Add(candidateDiscounts.Items.Select(pair => pair.Key).JoinAsString(";")))
        {
            return false;
        }

        CandidateDiscountsQueue.Enqueue(candidateDiscounts);

        return true;
    }

    public CandidateProductDiscounts Dequeue()
    {
        return CandidateDiscountsQueue.Dequeue();
    }
}