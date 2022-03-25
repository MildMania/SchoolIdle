using UnityEngine;

public class MoneyProducer : BaseProducer<Money>
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;
    [SerializeField] private DelivererFovController _delivererFovController;

    public override Money ProduceCustomActions(Money paper)
    {
        Debug.Log("Producing Money");
        return null;
    }

    protected override void TryRemoveAndGetLastProducibleCustomActions()
    {
    }
}