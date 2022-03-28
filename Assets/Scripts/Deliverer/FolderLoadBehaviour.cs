using UnityEngine;

public class FolderLoadBehaviour : BaseLoadBehaviour<FolderProducer, Folder>
{
	[SerializeField] private FolderProducerFovController _folderProducerFovController;
	
	private void Awake()
	{
		_folderProducerFovController.OnTargetEnteredFieldOfView += OnProducerEnteredFieldOfView;
		_folderProducerFovController.OnTargetExitedFieldOfView += OnProducerExitedFieldOfView;
	}


	private void OnDestroy()
	{
		_folderProducerFovController.OnTargetEnteredFieldOfView -= OnProducerEnteredFieldOfView;
		_folderProducerFovController.OnTargetExitedFieldOfView -= OnProducerExitedFieldOfView;


		StopAllCoroutines();
	}

	
	public override void LoadCustomActions(Folder resource)
	{
		Transform targetTransform = _updatedFormationController.GetLastTargetTransform(resource.transform);
		resource.Move(targetTransform, _updatedFormationController.Container);
		_deliverer.Resources.Add(resource);
	}
}