using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProducerManager : Singleton<ProducerBase>
{
    Dictionary<EStorableType, List<ProducerBase>> _unlockedProducersByStorableType = 
        new Dictionary<EStorableType, List<ProducerBase>>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        var storableTypes = (EStorableType[])System.Enum.GetValues(typeof(EStorableType));

        foreach (var storableType in storableTypes)
        {
            _unlockedProducersByStorableType[storableType] = new List<ProducerBase>();
        }
    }

    public void AddProducerToCurrentUnlockedList(ProducerBase producer)
    {
        _unlockedProducersByStorableType[producer.ProducedStorableType].Add(producer);
    }

}
