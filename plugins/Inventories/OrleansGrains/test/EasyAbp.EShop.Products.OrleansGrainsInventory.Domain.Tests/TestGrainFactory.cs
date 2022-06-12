using System;
using System.Threading.Tasks;
using EasyAbp.EShop.Plugins.Inventories.OrleansGrains;
using Microsoft.Extensions.DependencyInjection;
using Orleans;
using Orleans.Runtime;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.EShop.Products.OrleansGrainsInventory.Domain;

[Dependency(ReplaceServices = true)]
public class TestGrainFactory : IGrainFactory, ITransientDependency
{
    private IInventoryGrain Grain { get; set; }

    private readonly IServiceProvider _serviceProvider;

    public TestGrainFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public TGrainInterface GetGrain<TGrainInterface>(Guid primaryKey, string grainClassNamePrefix = null)
        where TGrainInterface : IGrainWithGuidKey
    {
        throw new NotSupportedException();
    }

    public TGrainInterface GetGrain<TGrainInterface>(long primaryKey, string grainClassNamePrefix = null)
        where TGrainInterface : IGrainWithIntegerKey
    {
        throw new NotSupportedException();
    }

    public TGrainInterface GetGrain<TGrainInterface>(string primaryKey, string grainClassNamePrefix = null)
        where TGrainInterface : IGrainWithStringKey
    {
        if (typeof(TGrainInterface) != typeof(IInventoryGrain))
        {
            throw new ApplicationException();
        }

        if (Grain is not null)
        {
            return (TGrainInterface)Grain;
        }

        Grain = _serviceProvider.GetRequiredService<FakeInventoryGrain>();

        return (TGrainInterface)Grain;
    }

    public TGrainInterface GetGrain<TGrainInterface>(Guid primaryKey, string keyExtension,
        string grainClassNamePrefix = null) where TGrainInterface : IGrainWithGuidCompoundKey
    {
        throw new NotSupportedException();
    }

    public TGrainInterface GetGrain<TGrainInterface>(long primaryKey, string keyExtension,
        string grainClassNamePrefix = null) where TGrainInterface : IGrainWithIntegerCompoundKey
    {
        throw new NotSupportedException();
    }

    public async Task<TGrainObserverInterface> CreateObjectReference<TGrainObserverInterface>(IGrainObserver obj)
        where TGrainObserverInterface : IGrainObserver
    {
        throw new NotSupportedException();
    }

    public async Task DeleteObjectReference<TGrainObserverInterface>(IGrainObserver obj)
        where TGrainObserverInterface : IGrainObserver
    {
        throw new NotSupportedException();
    }

    public void BindGrainReference(IAddressable grain)
    {
        throw new NotSupportedException();
    }

    public TGrainInterface GetGrain<TGrainInterface>(Type grainInterfaceType, Guid grainPrimaryKey)
        where TGrainInterface : IGrain
    {
        throw new NotSupportedException();
    }

    public TGrainInterface GetGrain<TGrainInterface>(Type grainInterfaceType, long grainPrimaryKey)
        where TGrainInterface : IGrain
    {
        throw new NotSupportedException();
    }

    public TGrainInterface GetGrain<TGrainInterface>(Type grainInterfaceType, string grainPrimaryKey)
        where TGrainInterface : IGrain
    {
        throw new NotSupportedException();
    }

    public TGrainInterface GetGrain<TGrainInterface>(Type grainInterfaceType, Guid grainPrimaryKey, string keyExtension)
        where TGrainInterface : IGrain
    {
        throw new NotSupportedException();
    }

    public TGrainInterface GetGrain<TGrainInterface>(Type grainInterfaceType, long grainPrimaryKey, string keyExtension)
        where TGrainInterface : IGrain
    {
        throw new NotSupportedException();
    }

    public IGrain GetGrain(Type grainInterfaceType, Guid grainPrimaryKey)
    {
        throw new NotSupportedException();
    }

    public IGrain GetGrain(Type grainInterfaceType, long grainPrimaryKey)
    {
        throw new NotSupportedException();
    }

    public IGrain GetGrain(Type grainInterfaceType, string grainPrimaryKey)
    {
        throw new NotSupportedException();
    }

    public IGrain GetGrain(Type grainInterfaceType, Guid grainPrimaryKey, string keyExtension)
    {
        throw new NotSupportedException();
    }

    public IGrain GetGrain(Type grainInterfaceType, long grainPrimaryKey, string keyExtension)
    {
        throw new NotSupportedException();
    }
}