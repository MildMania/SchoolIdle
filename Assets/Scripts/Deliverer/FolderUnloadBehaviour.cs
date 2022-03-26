using UnityEngine;

public class FolderUnloadBehaviour : BaseUnloadBehaviour<FolderConsumer, Folder>
{
	[SerializeField] private FolderConsumerFovController _folderConsumerFovController;
	private void Awake()
	{
		_folderConsumerFovController.OnTargetEnteredFieldOfView += OnConsumerEnteredFieldOfView;
		_folderConsumerFovController.OnTargetExitedFieldOfView += OnConsumerEnteredFieldOfView;
	}


	private void OnDestroy()
	{
		_folderConsumerFovController.OnTargetEnteredFieldOfView -= OnConsumerEnteredFieldOfView;
		_folderConsumerFovController.OnTargetExitedFieldOfView -= OnConsumerEnteredFieldOfView;
		
		StopAllCoroutines();
	}

	public override void UnloadCustomActions(int index)
	{
		//Remove from self
		Folder folder = (Folder)_deliverer.Resources[_deliverer.Resources.Count - 1];
		_deliverer.Resources.Remove(folder);
		_updatedFormationController.RemoveAndGetLastTransform();

		//Add To Consumer
		FolderConsumer paperConsumer = _consumers[index];
		UpdatedFormationController consumerFormationController =
			paperConsumer.GetComponentInChildren<UpdatedFormationController>();
		Transform targetTransform = consumerFormationController.GetLastTargetTransform(folder.transform);
		folder.Move(targetTransform, consumerFormationController.Container);
		paperConsumer.ResourceProvider.Resources.Add(folder);
	}
}