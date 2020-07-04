using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Baskets.ProductUpdates;
using EasyAbp.EShop.Products.ProductInventories;
using EasyAbp.EShop.Products.Products;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events.Distributed;
using Volo.Abp.Guids;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.EShop.Plugins.Baskets.BasketItems
{
    public class ProductUpdateRecorder : IProductUpdateRecorder, ITransientDependency
    {
        private readonly IGuidGenerator _guidGenerator;
        private readonly IProductUpdateRepository _productUpdateRepository;

        public ProductUpdateRecorder(
            IGuidGenerator guidGenerator,
            IProductUpdateRepository productUpdateRepository)
        {
            _guidGenerator = guidGenerator;
            _productUpdateRepository = productUpdateRepository;
        }
        
        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(EntityUpdatedEto<ProductEto> eventData)
        {
            foreach (var skuId in eventData.Entity.ProductSkus.Select(sku => sku.Id))
            {
                await UpdateAsync(skuId);
            }
        }

        [UnitOfWork(true)]
        public virtual async Task HandleEventAsync(ProductInventoryChangedEto eventData)
        {
            await UpdateAsync(eventData.ProductSkuId);
        }
        
        protected virtual async Task UpdateAsync(Guid skuId)
        {
            var entity = await _productUpdateRepository.FindAsync(x => x.ProductSkuId == skuId);

            if (entity == null)
            {
                entity = new ProductUpdate(_guidGenerator.Create(), skuId);

                await _productUpdateRepository.InsertAsync(entity);
            }
            else
            {
                await _productUpdateRepository.UpdateAsync(entity);
            }
        }
    }
}