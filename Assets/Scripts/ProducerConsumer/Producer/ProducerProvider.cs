using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerProvider : Singleton<ProducerProvider>
{
    Dictionary<System.Type, List<IAIInteractable>> _producersByResource =
        new Dictionary<System.Type, List<IAIInteractable>>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {

    }

    public void AddProducer(IAIInteractable producer, System.Type resourceType)
    {
        List<IAIInteractable> list;
        System.Type resType = resourceType;

        _producersByResource.TryGetValue(resType, out list);

        if (list != null)
        {
            _producersByResource[resType].Add(producer);
        }
        else
        {
            _producersByResource[resType] = new List<IAIInteractable>();
            _producersByResource[resType].Add(producer);
        }

    }


    public List<IAIInteractable> GetProducers(System.Type resourceType)
    {
        List<IAIInteractable> list;
        _producersByResource.TryGetValue(resourceType, out list);

        return list;
    }

}
