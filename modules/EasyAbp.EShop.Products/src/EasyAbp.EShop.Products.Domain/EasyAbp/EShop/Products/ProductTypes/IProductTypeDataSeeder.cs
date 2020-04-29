using System.Threading.Tasks;
using Volo.Abp.Data;

namespace EasyAbp.EShop.Products.ProductTypes
{
    public interface IProductTypeDataSeeder
    {
        Task SeedAsync(DataSeedContext context);
    }
}