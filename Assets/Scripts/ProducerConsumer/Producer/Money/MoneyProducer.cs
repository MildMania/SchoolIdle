using UnityEngine;

public class MoneyProducer : BaseProducer<Money>
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;
    
    public override Money ProduceCustomActions(Money money)
    {
        Transform targetTransform = _updatedFormationController.GetLastTargetTransform(money.transform);

        Money clonedMoneyProducible = Instantiate(money, money.transform.position, money.transform.rotation);

        clonedMoneyProducible.MoveProducible(targetTransform, _updatedFormationController.Container);
        
        return clonedMoneyProducible;
    }

    protected override void TryRemoveAndGetLastProducibleCustomActions()
    {
    }
}