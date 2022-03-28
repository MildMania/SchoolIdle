using UnityEngine;

public class FolderProducer : BaseProducer<Folder>
{
	[SerializeField] private UpdatedFormationController _updatedFormationController;
	public override Folder ProduceCustomActions(Folder resource)
	{
		Transform targetTransform = _updatedFormationController.GetLastTargetTransform(resource.transform);

		Folder clonedFolderProducible = Instantiate(resource, resource.transform.position, resource.transform.rotation);

		clonedFolderProducible.Move(targetTransform, _updatedFormationController.Container);
		return clonedFolderProducible;
	}

	protected override void TryRemoveAndGetLastProducibleCustomActions()
	{
		_updatedFormationController.RemoveAndGetLastTransform();
	}
}