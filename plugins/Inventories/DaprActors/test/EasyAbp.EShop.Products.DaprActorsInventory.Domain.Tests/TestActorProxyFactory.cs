using System;
using Dapr.Actors;
using Dapr.Actors.Client;
using EasyAbp.EShop.Plugins.Inventories.DaprActors;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.DaprActorsInventory;

[Dependency(ReplaceServices = true)]
public class TestActorProxyFactory : IActorProxyFactory, ITransientDependency
{
    private IInventoryActor Actor { get; set; }

    private readonly IServiceProvider _serviceProvider;

    public TestActorProxyFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TActorInterface CreateActorProxy<TActorInterface>(ActorId actorId, string actorType,
        ActorProxyOptions options = null) where TActorInterface : IActor
    {
        if (typeof(TActorInterface) != typeof(IInventoryActor))
        {
            throw new ApplicationException();
        }
        
        if (Actor is not null)
        {
            return (TActorInterface)Actor;
        }

        Actor = _serviceProvider.GetRequiredService<IInventoryActor>();

        return (TActorInterface)Actor;
    }

    public object CreateActorProxy(ActorId actorId, Type actorInterfaceType, string actorType,
        ActorProxyOptions options = null)
    {
        throw new NotSupportedException();
    }

    public ActorProxy Create(ActorId actorId, string actorType, ActorProxyOptions options = null)
    {
        throw new NotSupportedException();
    }
}