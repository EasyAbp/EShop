using Xunit;

namespace EasyAbp.EShop.Plugins.StoreApproval.MongoDB
{
    [CollectionDefinition(Name)]
    public class MongoTestCollection : ICollectionFixture<MongoDbFixture>
    {
        public const string Name = "MongoDB Collection";
    }
}