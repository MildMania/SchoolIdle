using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHelper : MonoBehaviour
{
    [MMSerializedInterface(typeof(IResource))] private IResource _resource;

    public List<IAIInteractable> GetConsumers()
    {
        return ConsumerProvider.Instance.GetConsumers(_resource);
    }

    public List<IAIInteractable> GetProducers()
    {
        return ProducerProvider.Instance.GetProducers(_resource);
    }
}
