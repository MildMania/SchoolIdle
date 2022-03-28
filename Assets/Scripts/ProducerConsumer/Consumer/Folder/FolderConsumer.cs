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
        Folder folder = (Folder) resource;
        folder.OnMoveRoutineFinished -= OnMoveRoutineFinished;
        folder.gameObject.SetActive(false);
        OnConsumeFinished?.Invoke(this, folder);
    }
}