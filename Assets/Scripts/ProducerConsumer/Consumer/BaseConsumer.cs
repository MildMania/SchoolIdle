using System;
using System.Collections;
using UnityEngine;

public abstract class BaseConsumer<TResource> : MonoBehaviour, IConsumer<TResource>
    where TResource : IResource
{
    [SerializeField] protected BaseResourceProvider<TResource> _baseResourceProvider;
    [SerializeField] protected UpdatedFormationController _updatedFormationController;

    public Action<BaseConsumer<TResource>, TResource> OnConsumeFinished;

    public BaseResourceProvider<TResource> ResourceProvider
    {
        get => _baseResourceProvider;
        set => _baseResourceProvider = value;
    }

    public void Consume(TResource resource)
    {
        ResourceProvider.Resources.Remove(resource);
        ConsumeCustomActions(resource);
        _updatedFormationController.RemoveAndGetLastTransform();
    }

    public abstract void ConsumeCustomActions(TResource resource);
}