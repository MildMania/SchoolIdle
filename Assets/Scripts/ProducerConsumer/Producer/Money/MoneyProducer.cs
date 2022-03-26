using UnityEngine;

public class MoneyProducer : BaseProducer<Money>
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;

    public override Money ProduceCustomActions(Money resource)
    {
        Transform targetTransform = _updatedFormationController.GetLastTargetTransform(resource.transform);

        Money clonedMoneyProducible = Instantiate(resource, resource.transform.position, resource.transform.rotation);

        clonedMoneyProducible.Move(targetTransform, _updatedFormationController.Container);

        return clonedMoneyProducible;
    }

    protected override void TryRemoveAndGetLastProducibleCustomActions()
    {
        _updatedFormationController.RemoveAndGetLastTransform();
    }
}