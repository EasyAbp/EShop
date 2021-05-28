namespace EasyAbp.EShop.Inventory.Warehouses
{
    using EasyAbp.EShop.Inventory.Warehouses.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 库房控制器
    /// </summary>
    [RemoteService(Name = "Inventory")]
    [Route("/api/e-shop/inventory/warehouse")]
    public class WarehouseController : InventoryController, IWarehouseAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IWarehouseAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="WarehouseController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IWarehouseAppService"/>.</param>
        public WarehouseController(IWarehouseAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="WarehouseCreateDto"/>.</param>
        /// <returns>The <see cref="Task{WarehouseDto}"/>.</returns>
        [HttpPost]
        public Task<WarehouseDto> CreateAsync(WarehouseCreateDto input)
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
        /// <returns>The <see cref="Task{WarehouseDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<WarehouseDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="PagedAndSortedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{WarehouseDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<WarehouseDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="WarehouseUpdateDto"/>.</param>
        /// <returns>The <see cref="Task{WarehouseDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<WarehouseDto> UpdateAsync(Guid id, WarehouseUpdateDto input)
        {
            return _service.UpdateAsync(id, input);
        }
    }
}
