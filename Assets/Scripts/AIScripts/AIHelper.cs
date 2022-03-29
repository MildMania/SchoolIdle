using System.Collections;
using System.Collections.Generic;
using MMFramework.Tasks.Examples;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class AIHelper : SerializedMonoBehaviour
{
    [OdinSerialize] private IResource _resource;

    private void Awake()
    {
        
    }

    public List<IAIInteractable> GetConsumers()
    {
        return ConsumerProvider.Instance.GetConsumers(_resource.GetType());
    }

    public List<IAIInteractable> GetProducers()
    {
        return ProducerProvider.Instance.GetProducers(_resource.GetType());
    }
}
