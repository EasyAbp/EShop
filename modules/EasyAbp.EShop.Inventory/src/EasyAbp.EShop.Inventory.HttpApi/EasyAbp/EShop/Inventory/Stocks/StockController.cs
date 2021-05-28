namespace EasyAbp.EShop.Inventory.Stocks
{
    using EasyAbp.EShop.Inventory.Stocks.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 库存存品控制器
    /// </summary>
    [RemoteService(Name = "Inventory")]
    [Route("/api/e-shop/inventory/stock")]
    public class StockController : InventoryController, IStockAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IStockAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="StockController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IStockAppService"/>.</param>
        public StockController(IStockAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="StockCreateDto"/>.</param>
        /// <returns>The <see cref="Task{StockDto}"/>.</returns>
        [HttpPost]
        public Task<StockDto> CreateAsync(StockCreateDto input)
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
        /// <returns>The <see cref="Task{StockDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<StockDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="PagedAndSortedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{StockDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<StockDto>> GetListAsync(GetStockListInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="StockUpdateDto"/>.</param>
        /// <returns>The <see cref="Task{StockDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<StockDto> UpdateAsync(Guid id, StockUpdateDto input)
        {
            return _service.UpdateAsync(id, input);
        }
    }
}
