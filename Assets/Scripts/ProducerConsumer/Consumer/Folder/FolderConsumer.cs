using UnityEngine;

public class FolderConsumer : BaseConsumer<Folder>
{
	public override void ConsumeCustomActions(Folder folder)
	{
		Transform targetTransform = _updatedFormationController.GetFirstTargetTransform();
		folder.Move(targetTransform, null);
		folder.OnMoveRoutineFinished += OnMoveRoutineFinished;
		Debug.Log("FOLDER CONSUMED");
	}

	private void OnMoveRoutineFinished(IResource resource)
	{
		Folder paper = (Folder) resource;
		paper.OnMoveRoutineFinished -= OnMoveRoutineFinished;
		paper.gameObject.SetActive(false);
	}
	
	
}