using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumerManager : Singleton<ConsumerManager>
{
    Dictionary<EStorableType, List<ConsumerBase>> _unlockedConsumersByStorableType =
    new Dictionary<EStorableType, List<ConsumerBase>>();

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        var storableTypes = (EStorableType[])System.Enum.GetValues(typeof(EStorableType));

        foreach (var storableType in storableTypes)
        {
            _unlockedConsumersByStorableType[storableType] = new List<ConsumerBase>();
        }
    }

    public void AddConsumerToCurrentUnlockedList(ConsumerBase consumer)
    {
        _unlockedConsumersByStorableType[consumer.ConsumeType].Add(consumer);
    }
}
