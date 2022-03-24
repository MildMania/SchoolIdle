using System.Collections.Generic;
using UnityEngine;

public abstract class BaseProducer<TProducible> : MonoBehaviour, IProducer<TProducible>
    where TProducible : IProducible
{
    protected List<IProducible> _producibles = new List<IProducible>();

    public void Produce(TProducible producible)
    {
        ProduceCustomActions(producible);
        _producibles.Add(producible);
    }

    public abstract void ProduceCustomActions(TProducible paper);
}