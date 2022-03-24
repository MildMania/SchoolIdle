using UnityEngine;

public class MoneyProducer : BaseProducer<Money>
{
    public override void ProduceCustomActions(Money paper)
    {
        Debug.Log("Producing Money");
    }
}