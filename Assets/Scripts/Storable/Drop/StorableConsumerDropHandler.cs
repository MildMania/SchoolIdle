using UnityEngine;

public class StorableConsumerDropHandler : StorableDropHandler
{

    [SerializeField] private ConsumerBase _consumerBase;
    
    
    protected override void OnAwakeCustomActions()
    {
        base.OnAwakeCustomActions();
        _consumerBase.OnConsumed += OnConsumerConsumed;
    }

    private void OnConsumerConsumed(StorableBase storable)
    {
        _storableController.StorableList.Remove(storable);
        DropStorable(storable);
    }
    

    protected override void OnOnDestroyCustomActions()
    {
        base.OnOnDestroyCustomActions();
        _consumerBase.OnConsumed -= OnConsumerConsumed;
    }
}