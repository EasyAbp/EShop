using System;
using Volo.Abp.Domain.Repositories;

namespace EasyAbp.EShop.Plugins.Promotions.Promotions;

public interface IPromotionRepository : IRepository<Promotion, Guid>
{
}
