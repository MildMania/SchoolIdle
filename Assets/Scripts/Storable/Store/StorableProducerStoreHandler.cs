using UnityEngine;

public class StorableProducerStoreHandler : StorableStoreHandler
{
    [SerializeField] private ProducerBase _producerBase;

    private void Awake()
    {
        _producerBase.OnProduced += OnProduced;
    }

    private void OnDestroy()
    {
        _producerBase.OnProduced -= OnProduced;
    }
    
    private void OnProduced(StorableBase storable)
    {
        StoreStorable(storable);
    }
}
