using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableTranslator : MonoBehaviour
{
    [SerializeField] private ConsumerBase _consumerBase;
    [SerializeField] private StorableConsumerDropHandler _storableConsumerDropHandler;

    private void Awake()
    {
        _consumerBase.OnConsumed += OnConsumerConsumed;
        _storableConsumerDropHandler.OnStorableDropped += OnStorableDropped;
        
     
    }

    private void OnStorableDropped(StorableBase storable)
    {
        
        _storableConsumerDropHandler.StopDrop();
        //GOLD PRODUCE BEGIN!
    }

    private void OnConsumerConsumed(StorableBase storable)
    {
        _storableConsumerDropHandler.StartDrop();
    }

    private void OnDestroy()
    {
        _consumerBase.OnConsumed -= OnConsumerConsumed;
        _storableConsumerDropHandler.OnStorableDropped -= OnStorableDropped;
        
   
    }
}
