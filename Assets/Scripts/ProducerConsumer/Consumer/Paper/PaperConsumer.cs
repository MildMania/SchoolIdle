using System;
using UnityEngine;

public class PaperConsumer : BaseConsumer<Paper>
{
    public Action<Paper> OnConsumed;

    public override void ConsumeCustomActions(Paper paper)
    {
        Transform targetTransform = _updatedFormationController.GetLastTargetTransform(paper.transform);
        paper.MoveConsumable(targetTransform, _updatedFormationController.Container);
        Debug.Log("PAPER CONSUMED");
        OnConsumed?.Invoke(paper);
    }
}