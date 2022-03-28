using UnityEngine;

public class PaperProducer : BaseProducer<Paper>
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;

    public override Paper ProduceCustomActions(Paper resource)
    {
        Transform targetTransform = _updatedFormationController.GetLastTargetTransform(resource.transform);

        Paper clonedPaperProducible = Instantiate(resource, resource.transform.position, resource.transform.rotation);

        clonedPaperProducible.Move(targetTransform, _updatedFormationController.Container);
        return clonedPaperProducible;
    }

    protected override void TryRemoveAndGetLastProducibleCustomActions()
    {
        _updatedFormationController.RemoveAndGetLastTransform();
    }
}