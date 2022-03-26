using UnityEngine;

[RequireComponent(typeof(FolderProducerFovController))]
public class FovBasedFolderProducerDetector : BaseFolderProducerDetector
{
	[SerializeField] private FolderProducerFovController _folderProducerFOVController;

	private void Awake()
	{
		SubscribeToFovController();
		_folderProducerFOVController.SetActive(true);
	}

	private void OnDestroy()
	{
		_folderProducerFOVController.SetActive(false);
		UnsubscribeFromFovController();
	}

	private void SubscribeToFovController()
	{
		_folderProducerFOVController.OnTargetEnteredFieldOfView += OnTargetEnteredFieldOfView;
		_folderProducerFOVController.OnTargetExitedFieldOfView += OnTargetExitedFieldOfView;
	}

	private void UnsubscribeFromFovController()
	{
		_folderProducerFOVController.OnTargetEnteredFieldOfView -= OnTargetEnteredFieldOfView;
		_folderProducerFOVController.OnTargetExitedFieldOfView -= OnTargetExitedFieldOfView;
	}

	private void OnTargetEnteredFieldOfView(FolderProducer folderProducer)
	{
		LastDetected = folderProducer;
		OnDetected?.Invoke(folderProducer);
	}

	private void OnTargetExitedFieldOfView(FolderProducer folderProducer)
	{
		OnEnded?.Invoke(folderProducer);
	}
}