using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using MMFramework.Tasks.Examples;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class AIHelper : SerializedMonoBehaviour
{
    [OdinSerialize] private IResource _resource;
    public IResource Resource => _resource;

    [OdinSerialize] private Dictionary<IResource, BaseLoadBehaviour> _resourceToLoadBehaviour;

    private BaseLoadBehaviour _currentLoadBehaviour;
    public BaseLoadBehaviour CurrentLoadBehaviour => _currentLoadBehaviour;


    private void Awake()
    {
        foreach (var item in _resourceToLoadBehaviour)
        {
            GameObject obj = item.Value.gameObject;

            if (!item.Key.Equals(_resource))
                obj.SetActive(false);
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
