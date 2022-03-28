using System;
using System.Collections.Generic;
using UnityEngine;

public class PaperToFolderProductionRequirement : BaseProductionRequirement
{
    [SerializeField] private int _amountNeeded;
    [SerializeField] private PaperProvider _paperProvider;
    [SerializeField] private PaperConsumptionController _paperConsumptionController;

    public override bool IsProductionRequirementMet()
    {
        List<Paper> papers = _paperProvider.Resources;
        if (!_paperConsumptionController.IsAvailable || papers == null || papers.Count < _amountNeeded)
        {
            return false;
        }

        return true;
    }

    public override void ConsumeRequirements(Action onConsumedCallback)
    {
        _paperConsumptionController.StartConsumption(_amountNeeded, onConsumedCallback);
    }
}