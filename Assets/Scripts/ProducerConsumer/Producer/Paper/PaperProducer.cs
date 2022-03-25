using UnityEngine;

public class PaperProducer : BaseProducer<Paper>
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;

    public override Paper ProduceCustomActions(Paper paper)
    {
        Transform targetTransform = _updatedFormationController.GetLastTargetTransform(paper.transform);

        Paper clonedPaperProducible = Instantiate(paper, paper.transform.position, paper.transform.rotation);

        clonedPaperProducible.MoveProducible(targetTransform, _updatedFormationController.Container);
        return clonedPaperProducible;
    }

    protected override void TryRemoveAndGetLastProducibleCustomActions()
    {
        _updatedFormationController.RemoveAndGetLastTransform();
    }
}