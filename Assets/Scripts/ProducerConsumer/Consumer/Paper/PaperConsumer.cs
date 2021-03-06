using System;
using UnityEngine;

public class PaperConsumer : BaseConsumer<Paper>
{
    public override void ConsumeCustomActions(Paper paper)
    {
        Transform targetTransform = _updatedFormationController.GetFirstTargetTransform();
        paper.Move(targetTransform, null);
        paper.OnMoveRoutineFinished += OnMoveRoutineFinished;
    }

    private void OnMoveRoutineFinished(IResource resource)
    {
        Paper paper = (Paper) resource;
        paper.OnMoveRoutineFinished -= OnMoveRoutineFinished;
        paper.gameObject.SetActive(false);
        Debug.Log("PAPER CONSUMED");
        OnConsumeFinished?.Invoke(this, paper);
    }
}