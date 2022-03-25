using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProducer<TProducible> : MonoBehaviour, IProducer<TProducible>
    where TProducible : IProducible
{
    protected List<TProducible> _producibles = new List<TProducible>();

    public void Produce(TProducible producible)
    {
        _producibles.Add(ProduceCustomActions(producible));
    }

    public abstract TProducible ProduceCustomActions(TProducible producible);


    public bool TryRemoveAndGetLastProducible(ref TProducible lastProducible)
    {
        if (_producibles.Count == 0)
        {
            return false;
        }

        lastProducible = _producibles[_producibles.Count - 1];
        _producibles.Remove(lastProducible);
        TryRemoveAndGetLastProducibleCustomActions();
        return true;
    }


    protected abstract void TryRemoveAndGetLastProducibleCustomActions();
}