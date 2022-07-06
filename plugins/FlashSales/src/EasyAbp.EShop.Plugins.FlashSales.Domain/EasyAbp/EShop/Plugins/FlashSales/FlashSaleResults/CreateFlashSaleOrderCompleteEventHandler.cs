using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalePlans;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSaleResults;

public class CreateFlashSaleOrderCompleteEventHandler : IDistributedEventHandler<CreateFlashSaleOrderCompleteEto>, ITransientDependency
{
    protected IFlashSaleResultRepository FlashSaleResultRepository { get; }
    protected IGuidGenerator GuidGenerator { get; }

    public CreateFlashSaleOrderCompleteEventHandler(IFlashSaleResultRepository flashSaleResultRepository, IGuidGenerator guidGenerator)
    {
        FlashSaleResultRepository = flashSaleResultRepository;
        GuidGenerator = guidGenerator;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(CreateFlashSaleOrderCompleteEto eventData)
    {
        var flashSaleResult = await FlashSaleResultRepository.GetAsync(eventData.PendingResultId);

        if (eventData.Success)
        {
            flashSaleResult.MarkAsSuccessful(eventData.OrderId.Value);
        }
        else
        {
            flashSaleResult.MarkAsFailed(eventData.Reason);
        }

        await FlashSaleResultRepository.UpdateAsync(flashSaleResult, autoSave: true);
    }
}
