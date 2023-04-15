using System;
using System.Collections.Generic;
using System.Linq;
using EasyAbp.EShop.Products.Products;
using Volo.Abp;

namespace EasyAbp.EShop.Orders.Orders;

public class OrderDiscountElectionModel
{
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

    public IOrder Order { get; }

    public Dictionary<Guid, IProduct> ProductDict { get; }

    public List<OrderDiscountsSchemeModel> Schemes { get; } = new();

    public bool Done => Schemes.Any() && !CandidateDiscountsQueue.Any();

    private Queue<CandidateOrderDiscounts> CandidateDiscountsQueue { get; } = new();

    private HashSet<string> UsedCombinations { get; } = new();

    public OrderDiscountElectionModel(IOrder order, Dictionary<Guid, IProduct> productDict)
    {
        Order = order;
        ProductDict = productDict;
    }

    public OrderDiscountsSchemeModel GetBestScheme()
    {
        if (!Done)
        {
            throw new AbpException("The OrderDiscountElectionModel is in an incorrect state.");
        }

        return Schemes.MaxBy(x => x.TotalDiscountAmount);
    }

    public bool TryEnqueue(CandidateOrderDiscounts candidateDiscounts)
    {
        if (candidateDiscounts.Items.Count > MaxCandidates)
        {
            throw new AbpException(
                $"The OrderDiscountElectionModel cannot handle a CandidateOrderDiscounts with the number of candidate discounts > {MaxCandidates}.");
        }

        CurrentDepth++;

        if (CurrentDepth > MaxDepth || CurrentDepth < 0)
        {
            throw new AbpException("The OrderDiscountElectionModel's search exceeded maximum depth.");
        }

        if (!UsedCombinations.Add(candidateDiscounts.Items.Select(pair => pair.Key).JoinAsString(";")))
        {
            return false;
        }

        CandidateDiscountsQueue.Enqueue(candidateDiscounts);

        return true;
    }

    public CandidateOrderDiscounts Dequeue()
    {
        return CandidateDiscountsQueue.Dequeue();
    }
}