using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;


public abstract class
    BaseAIHelper<TBaseUnloadBehaviour, TBaseLoadBehaviour, TBaseConsumer, TBaseProducer,
        TResource> : SerializedMonoBehaviour where
    TBaseUnloadBehaviour :
    BaseUnloadBehaviour<TBaseConsumer, TResource>
    where TBaseConsumer :
    BaseConsumer<TResource>
    where TBaseLoadBehaviour :
    BaseLoadBehaviour<TBaseProducer, TResource>
    where TBaseProducer : BaseProducer<TResource>
    where TResource : IResource
{
    [OdinSerialize] protected TResource _tResource;
}