using System;
using UnityEngine;

public class PaperConsumer : BaseConsumer<Paper>
{
    public Action<Paper> OnConsumed;

    public override void ConsumeCustomActions(Paper paper)
    {
        paper.transform.SetParent(null);
        paper.transform.gameObject.SetActive(false);

        // Transform targetTransform = _updatedFormationController.GetLastTargetTransform(paper.transform);
        // paper.Move(targetTransform, _updatedFormationController.Container);
        Debug.Log("PAPER CONSUMED");
        OnConsumed?.Invoke(paper);
    }
}