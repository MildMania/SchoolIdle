using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProduceCapacityRequirement : BaseProductionRequirement
{
    [SerializeField] private int _produceCapacity;
    [SerializeField] private UpdatedFormationController _updatedFormationController;

    private int _producedCount;
    
    public override bool IsProductionRequirementMet()
    {
        _producedCount = _updatedFormationController.Container.childCount;
        
        return _producedCount < _produceCapacity;
    }

    public override void ConsumeRequirements(Action onConsumedCallback)
    {
        onConsumedCallback();
    }
}
