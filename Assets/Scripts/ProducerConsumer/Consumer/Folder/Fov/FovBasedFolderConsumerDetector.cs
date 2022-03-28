using UnityEngine;

[RequireComponent(typeof(FolderConsumerFovController))]
public class FovBasedFolderConsumerDetector : BaseFolderConsumerDetector
{
	[SerializeField] private FolderConsumerFovController _folderConsumerFOVController;

	private void Awake()
	{
		SubscribeToFovController();
		_folderConsumerFOVController.SetActive(true);
	}

	private void OnDestroy()
	{
		_folderConsumerFOVController.SetActive(false);
		UnsubscribeFromFovController();
	}

	private void SubscribeToFovController()
	{
		_folderConsumerFOVController.OnTargetEnteredFieldOfView += OnTargetEnteredFieldOfView;
		_folderConsumerFOVController.OnTargetExitedFieldOfView += OnTargetExitedFieldOfView;
	}

	private void UnsubscribeFromFovController()
	{
		_folderConsumerFOVController.OnTargetEnteredFieldOfView -= OnTargetEnteredFieldOfView;
		_folderConsumerFOVController.OnTargetExitedFieldOfView -= OnTargetExitedFieldOfView;
	}

	private void OnTargetEnteredFieldOfView(FolderConsumer folderConsumer)
	{
		LastDetected = folderConsumer;
		OnDetected?.Invoke(folderConsumer);
	}

	private void OnTargetExitedFieldOfView(FolderConsumer folderConsumer)
	{
		OnEnded?.Invoke(folderConsumer);
	}
}