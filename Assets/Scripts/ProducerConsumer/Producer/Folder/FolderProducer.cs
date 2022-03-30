using UnityEngine;

public class FolderProducer : BaseProducer<Folder>, IResourceProvider
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;

    public override Folder ProduceCustomActions(Folder folder)
    {
        Transform targetTransform = _updatedFormationController.GetLastTargetTransform(folder.transform);

        Folder clonedFolder = Instantiate(folder, folder.transform.position, folder.transform.rotation);

        clonedFolder.Move(targetTransform, _resourceProvider.ResourceContainer);
        return clonedFolder;
    }

    protected override void TryRemoveAndGetLastProducibleCustomActions()
    {
        _updatedFormationController.RemoveAndGetLastTransform();
    }
}