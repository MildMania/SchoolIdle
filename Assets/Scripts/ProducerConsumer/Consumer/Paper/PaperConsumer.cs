using System;
using UnityEngine;

public class PaperConsumer : BaseConsumer<Paper>
{
    public Action<Paper> OnConsumed;

    public override void ConsumeCustomActions(Paper paper)
    {
        Transform targetTransform = _updatedFormationController.GetFirstTargetTransform();
        paper.Move(targetTransform, null);
        paper.OnMoveRoutineFinished += OnMoveRoutineFinished;
        Debug.Log("PAPER CONSUMED");
        OnConsumed?.Invoke(paper);
    }

    private void OnMoveRoutineFinished(IResource resource)
    {
        Paper paper = (Paper) resource;
        paper.OnMoveRoutineFinished -= OnMoveRoutineFinished;
        paper.gameObject.SetActive(false);
    }
}