using UnityEngine;

public class FolderUnloadBehaviour : BaseUnloadBehaviour<FolderConsumer, Folder>
{
	[SerializeField] private FolderConsumerFovController _folderConsumerFovController;
	
	protected override void OnAwakeCustomActions()
	{
		base.OnAwakeCustomActions();
		
		_folderConsumerFovController.OnTargetEnteredFieldOfView += OnConsumerEnteredFieldOfView;
		_folderConsumerFovController.OnTargetExitedFieldOfView += OnConsumerExitedFieldOfView;
	}

	protected override void OnDestroyCustomActions()
	{
		base.OnDestroyCustomActions();
		
		_folderConsumerFovController.OnTargetEnteredFieldOfView -= OnConsumerEnteredFieldOfView;
		_folderConsumerFovController.OnTargetExitedFieldOfView -= OnConsumerExitedFieldOfView;
		
		StopAllCoroutines();
	}

	public override void UnloadCustomActions(int index)
	{
		int lastResourceIndex = _deliverer.GetLastResourceIndex<Folder>();

		if (lastResourceIndex == -1)
		{
			return;
		}
		
		//Remove from self
		Folder folder = (Folder)_deliverer.Resources[_deliverer.Resources.Count - 1];
		_deliverer.Resources.Remove(folder);
		_updatedFormationController.RemoveAndGetLastTransform();

		//Add To Consumer
		FolderConsumer folderConsumer = _consumers[index];
		UpdatedFormationController consumerFormationController =
			folderConsumer.GetComponentInChildren<UpdatedFormationController>();
		Transform targetTransform = consumerFormationController.GetLastTargetTransform(folder.transform);
		folder.Move(targetTransform, folderConsumer.ResourceProvider.ResourceContainer);
		folderConsumer.ResourceProvider.Resources.Add(folder);
	}
}