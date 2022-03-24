using UnityEngine;

public abstract class BaseConsumer<TConsumable> : MonoBehaviour, IConsumer<TConsumable>
    where TConsumable : IConsumable
{
    public void Consume(TConsumable consumable)
    {
        ConsumeCustomActions(consumable);
    }

    public abstract void ConsumeCustomActions(TConsumable consumable);
}