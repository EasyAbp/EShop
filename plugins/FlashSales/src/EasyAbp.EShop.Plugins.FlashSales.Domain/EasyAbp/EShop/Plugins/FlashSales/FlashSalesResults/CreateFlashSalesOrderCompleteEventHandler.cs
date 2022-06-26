using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.FlashSales.FlashSalesPlans;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.FlashSales.FlashSalesResults;

public class CreateFlashSalesOrderCompleteEventHandler : IDistributedEventHandler<CreateFlashSalesOrderCompleteEto>, ITransientDependency
{
    protected IFlashSalesResultRepository FlashSalesResultRepository { get; }
    protected IGuidGenerator GuidGenerator { get; }

    public CreateFlashSalesOrderCompleteEventHandler(IFlashSalesResultRepository flashSalesResultRepository, IGuidGenerator guidGenerator)
    {
        FlashSalesResultRepository = flashSalesResultRepository;
        GuidGenerator = guidGenerator;
    }

    [UnitOfWork]
    public virtual async Task HandleEventAsync(CreateFlashSalesOrderCompleteEto eventData)
    {
        var flashSalesResult = await FlashSalesResultRepository.GetAsync(eventData.PendingResultId);

        if (eventData.Success)
        {
            flashSalesResult.MarkAsSuccessful(eventData.OrderId.Value);
        }
        else
        {
            flashSalesResult.MarkAsFailed(eventData.Reason);
        }

        await FlashSalesResultRepository.UpdateAsync(flashSalesResult, autoSave: true);
    }
}
