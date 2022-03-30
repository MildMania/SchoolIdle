using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;

public class AIHelper : SerializedMonoBehaviour
{
    [OdinSerialize] private IResource _resource;
    public IResource Resource => _resource;

    [OdinSerialize] private Dictionary<IResource, BaseLoadBehaviour> _resourceToLoadBehaviour;

    private BaseLoadBehaviour _currentLoadBehaviour;
    public BaseLoadBehaviour CurrentLoadBehaviour => _currentLoadBehaviour;


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