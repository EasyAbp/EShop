namespace EasyAbp.EShop.Inventory.Instocks
{
    using EasyAbp.EShop.Inventory.Instocks.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 入库记录控制器
    /// </summary>
    [RemoteService(Name = "Inventory")]
    [Route("/api/e-shop/inventory/instock")]
    public class InstockController : InventoryController, IInstockAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IInstockAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="InstockController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IInstockAppService"/>.</param>
        public InstockController(IInstockAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="InstockCreateDto"/>.</param>
        /// <returns>The <see cref="Task{InstockDto}"/>.</returns>
        [HttpPost]
        public Task<InstockDto> CreateAsync(InstockCreateDto input)
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
        /// <returns>The <see cref="Task{InstockDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<InstockDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="PagedAndSortedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{InstockDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<InstockDto>> GetListAsync(GetInstockListInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="InstockUpdateDto"/>.</param>
        /// <returns>The <see cref="Task{InstockDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<InstockDto> UpdateAsync(Guid id, InstockUpdateDto input)
        {
            return _service.UpdateAsync(id, input);
        }
    }
}
