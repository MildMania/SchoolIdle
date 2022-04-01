using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

using MMFramework.Utilities;

public class AIHelper : SerializedMonoBehaviour
{
    [OdinSerialize] private IResource _resource;
    public IResource Resource => _resource;

    [OdinSerialize] private Dictionary<IResource, BaseLoadBehaviour> _resourceToLoadBehaviour;
    [OdinSerialize] private Dictionary<IResource, BaseUnloadBehaviour> _resourceToUnloadBehaviour;

    private BaseLoadBehaviour _currentLoadBehaviour;
    public BaseLoadBehaviour CurrentLoadBehaviour => _currentLoadBehaviour;

    private BaseUnloadBehaviour _currentUnloadBehaviour;
    public BaseUnloadBehaviour CurrentUnloadBehaviour => _currentUnloadBehaviour;


    private void Start()
    {
        foreach (var item in _resourceToLoadBehaviour)
        {
            if (!item.Key.Equals(_resource))
            {
                item.Value.StopLoading();
            }
            else
            {
                _currentLoadBehaviour = item.Value;
                _currentLoadBehaviour.Deactivate();
            }
        }

        foreach (var item in _resourceToUnloadBehaviour)
        {
            if (!item.Key.Equals(_resource))
            {
                item.Value.StopUnloading();
            }
            else
            {
                _currentUnloadBehaviour = item.Value;
                _currentUnloadBehaviour.Deactivate();
            }
        }
    }

    public List<BaseConsumer> GetConsumers()
    {
        return ConsumerProvider.Instance.GetConsumers(_resource.GetType());
    }

    public List<BaseProducer> GetProducers()
    {
        return ProducerProvider.Instance.GetProducers(_resource.GetType());
    }
}