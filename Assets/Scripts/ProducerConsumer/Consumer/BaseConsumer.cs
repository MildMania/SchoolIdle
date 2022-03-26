using UnityEngine;

public abstract class BaseConsumer<TResource> : MonoBehaviour, IConsumer<TResource>
    where TResource : IResource
{
    [SerializeField] protected BaseResourceProvider<TResource> _baseResourceProvider;
    [SerializeField] protected UpdatedFormationController _updatedFormationController;

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

    // public void UnconsumeLast()
    // {
    //     var lastConsumer = ResourceProvider.Resources[0];
    //     if (lastConsumer is Paper paper)
    //     {
    //         paper.transform.SetParent(null);
    //         paper.transform.gameObject.SetActive(false);
    //     }
    //
    //     ResourceProvider.Resources.Remove(lastConsumer);
    //     _updatedFormationController.RemoveAndGetLastTransform();
    // }

    public abstract void ConsumeCustomActions(TResource paper);
}