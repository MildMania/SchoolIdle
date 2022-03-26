using UnityEngine;

public abstract class BaseProducer<TResource> : MonoBehaviour, IProducer<TResource>
    where TResource : IResource
{
    [SerializeField] protected BaseResourceProvider<TResource> _baseResourceProvider;

    public void Produce(TResource resource)
    {
        _baseResourceProvider.Resources.Add(ProduceCustomActions(resource));
    }

    public abstract TResource ProduceCustomActions(TResource resource);


    public bool TryRemoveAndGetLastProducible(ref TResource lastResource)
    {
        if (_baseResourceProvider.Resources.Count == 0)
        {
            return false;
        }

        lastResource = _baseResourceProvider.Resources[_baseResourceProvider.Resources.Count - 1];
        _baseResourceProvider.Resources.Remove(lastResource);
        TryRemoveAndGetLastProducibleCustomActions();
        return true;
    }


    protected abstract void TryRemoveAndGetLastProducibleCustomActions();
}