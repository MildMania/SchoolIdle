using UnityEngine;

public class AssignmentWorthCollector : WorthCollector<AssignmentWorth>
{
    [SerializeField] private AssignmentController _assignmentController;

    protected override void CollectWorthCustomActions(AssignmentWorth assignmentWorth)
    {
        _assignmentController.Collect();
        base.CollectWorthCustomActions(assignmentWorth);
    }
}