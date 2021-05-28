namespace EasyAbp.EShop.Inventory.Reallocations
{
    using EasyAbp.EShop.Inventory.Reallocations.Dtos;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;
    using Volo.Abp;
    using Volo.Abp.Application.Dtos;

    /// <summary>
    /// 调库记录控制器
    /// </summary>
    [RemoteService(Name = "Inventory")]
    [Route("/api/e-shop/inventory/reallocation")]
    public class ReallocationController : InventoryController, IReallocationAppService
    {
        /// <summary>
        /// Defines the _service.
        /// </summary>
        private readonly IReallocationAppService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReallocationController"/> class.
        /// </summary>
        /// <param name="service">The service<see cref="IReallocationAppService"/>.</param>
        public ReallocationController(IReallocationAppService service)
        {
            _service = service;
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="input">The input<see cref="ReallocationCreateDto"/>.</param>
        /// <returns>The <see cref="Task{ReallocationDto}"/>.</returns>
        [HttpPost]
        public Task<ReallocationDto> CreateAsync(ReallocationCreateDto input)
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
        /// <returns>The <see cref="Task{ReallocationDto}"/>.</returns>
        [HttpGet]
        [Route("{id}")]
        public Task<ReallocationDto> GetAsync(Guid id)
        {
            return _service.GetAsync(id);
        }

        /// <summary>
        /// 列表
        /// </summary>
        /// <param name="input">The input<see cref="PagedAndSortedResultRequestDto"/>.</param>
        /// <returns>The <see cref="Task{PagedResultDto{ReallocationDto}}"/>.</returns>
        [HttpGet]
        public Task<PagedResultDto<ReallocationDto>> GetListAsync(GetReallocationListInput input)
        {
            return _service.GetListAsync(input);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <param name="input">The input<see cref="ReallocationUpdateDto"/>.</param>
        /// <returns>The <see cref="Task{ReallocationDto}"/>.</returns>
        [HttpPut]
        [Route("{id}")]
        public Task<ReallocationDto> UpdateAsync(Guid id, ReallocationUpdateDto input)
        {
            return _service.UpdateAsync(id, input);
        }
    }
}
