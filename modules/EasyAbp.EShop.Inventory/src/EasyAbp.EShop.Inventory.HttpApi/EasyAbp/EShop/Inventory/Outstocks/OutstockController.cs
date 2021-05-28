namespace EasyAbp.EShop.Inventory.Outstocks
{
    using EasyAbp.EShop.Inventory.Outstocks.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 出库记录控制器
    /// </summary>
    [RemoteService(Name = "Inventory")]
    [Route("/api/e-shop/inventory/outstock")]
    public class OutstockController : InventoryController, IOutstockAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IOutstockAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="OutstockController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IOutstockAppService"/>.</param>
        public OutstockController(IOutstockAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="OutstockCreateDto"/>.</param>
        /// <returns>The <see cref="Task{OutstockDto}"/>.</returns>
        [HttpPost]
        public Task<OutstockDto> CreateAsync(OutstockCreateDto input)
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
        /// <returns>The <see cref="Task{OutstockDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<OutstockDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="PagedAndSortedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{OutstockDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<OutstockDto>> GetListAsync(GetOutstockListInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="OutstockUpdateDto"/>.</param>
        /// <returns>The <see cref="Task{OutstockDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<OutstockDto> UpdateAsync(Guid id, OutstockUpdateDto input)
        {
            return _service.UpdateAsync(id, input);
        }
    }
}
