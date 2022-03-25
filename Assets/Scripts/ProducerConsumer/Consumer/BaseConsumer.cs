using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseConsumer<TConsumable> : MonoBehaviour, IConsumer<TConsumable>
    where TConsumable : IConsumable
{
    [SerializeField] protected UpdatedFormationController _updatedFormationController;
    protected List<TConsumable> _consumables = new List<TConsumable>();

    public List<TConsumable> Consumables => _consumables;

    public void Consume(TConsumable consumable)
    {
        _consumables.Add(consumable);
        ConsumeCustomActions(consumable);
    }

    public void UnconsumeLast()
    {
        var lastConsumer = _consumables[0];
        if (lastConsumer is Paper paper)
        {
            paper.transform.SetParent(null);
            paper.transform.gameObject.SetActive(false);
        }

        _consumables.Remove(lastConsumer);
        _updatedFormationController.RemoveAndGetLastTransform();
    }

    public abstract void ConsumeCustomActions(TConsumable consumable);
}