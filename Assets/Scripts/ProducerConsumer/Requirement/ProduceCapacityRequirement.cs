using System;
using UnityEngine;

public class ProduceCapacityRequirement : BaseProductionRequirement
{
    [SerializeField] private int _produceCapacity;
    [SerializeField] private Transform _container;
    private int _producedCount;

    public override bool IsProductionRequirementMet()
    {
        _producedCount = _container.GetComponentsInChildren<IResource>().Length;

        return _producedCount < _produceCapacity;
    }

    public override void ConsumeRequirements(Action onConsumedCallback)
    {
        onConsumedCallback();
    }
}