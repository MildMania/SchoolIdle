using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableCarrierStoreHandler : StorableStoreHandler
{
    [SerializeField] private BaseProducerDetector _baseProducerDetector;

    
    private void Awake()
    {
        _baseProducerDetector.OnDetected += OnProducerDetected;
        _baseProducerDetector.OnEnded += OnProducerEnded;
        
    }


    private void OnDestroy()
    {
        _baseProducerDetector.OnDetected -= OnProducerDetected;
        _baseProducerDetector.OnEnded -= OnProducerEnded;
        

    }

    private void OnProducerDetected(ProducerBase producerBase)
    {
        
        var storableDropHandler = producerBase.GetComponentInChildren<StorableDropHandler>();

        storableDropHandler.OnDropped += OnStorableDropped;
        storableDropHandler.StartDrop();
    }

    private void OnStorableDropped(StorableBase storableBase)
    {
        Store(storableBase);
    }

    private void OnProducerEnded(ProducerBase producerBase)
    {
        var storableDropHandler = producerBase.GetComponentInChildren<StorableDropHandler>();

        storableDropHandler.StopDrop();
        storableDropHandler.OnDropped -= OnStorableDropped;
    }
}