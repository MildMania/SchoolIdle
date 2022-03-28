using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerProvider : Singleton<ProducerProvider>
{
    Dictionary<IResource, List<IAIInteractable>> _producersByResource =
        new Dictionary<IResource, List<IAIInteractable>>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    public void AddProducer(IAIInteractable producer, IResource resourceType)
    {
        List<IAIInteractable> list;

        _producersByResource.TryGetValue(resourceType, out list);

        if (list != null)
        {
            _producersByResource[resourceType].Add(producer);
        }
        else
        {
            _producersByResource[resourceType] = new List<IAIInteractable>();
        }

    }


    public List<IAIInteractable> GetProducers(IResource resourceType)
    {
        List<IAIInteractable> list;
        _producersByResource.TryGetValue(resourceType, out list);

        return list;
    }

}
