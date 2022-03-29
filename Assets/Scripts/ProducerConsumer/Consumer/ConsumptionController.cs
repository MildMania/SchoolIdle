using System;
using MMFramework.Tasks.Examples;
using UnityEngine;

public class ConsumptionController<TConsumer, TResource> : MonoBehaviour where TConsumer : BaseConsumer<TResource>
    where TResource : IResource
{
    [SerializeField] protected TConsumer _consumer;
    [SerializeField] private BaseResourceProvider<TResource> _resourceProvider;

    public void StartConsumption(int amount, Action onConsumedCallback)
    {
        for (int i = 0; i < amount; i++)
        {
            TResource resource = _resourceProvider.Resources[_resourceProvider.Resources.Count - 1];
            //TODO: Think about if it is the best way to remove resource here!
            _consumer.Consume(resource);
        }
        onConsumedCallback?.Invoke();
    }
}