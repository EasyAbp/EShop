namespace EasyAbp.EShop.Inventory.StockHistories
{
    using EasyAbp.EShop.Inventory.StockHistories.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 库存历史记录控制器
    /// </summary>
    [RemoteService(Name = "Inventory")]
    [Route("/api/e-shop/inventory/stock-history")]
    public class StockHistoryController : InventoryController, IStockHistoryAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IStockHistoryAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockHistoryController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IStockHistoryAppService"/>.</param>
        public StockHistoryController(IStockHistoryAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="StockHistoryCreateDto"/>.</param>
        /// <returns>The <see cref="Task{StockHistoryDto}"/>.</returns>
        [HttpPost]
        public Task<StockHistoryDto> CreateAsync(StockHistoryCreateDto input)
        {
            return _service.CreateAsync(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        [HttpDelete]
        [Route("{id}")]
        public Task DeleteAsync(Guid id)
        {
            return _service.DeleteAsync(id);
        }

        /// <summary>
        /// 详情
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{StockHistoryDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<StockHistoryDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="PagedAndSortedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{StockHistoryDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<StockHistoryDto>> GetListAsync(GetStockHistoryListInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="StockHistoryUpdateDto"/>.</param>
        /// <returns>The <see cref="Task{StockHistoryDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<StockHistoryDto> UpdateAsync(Guid id, StockHistoryUpdateDto input)
        {
            return _service.UpdateAsync(id, input);
        }
    }
}
