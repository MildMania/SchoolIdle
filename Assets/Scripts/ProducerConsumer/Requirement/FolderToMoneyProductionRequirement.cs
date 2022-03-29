using System;
using System.Collections.Generic;
using UnityEngine;

public class FolderToMoneyProductionRequirement : BaseProductionRequirement
{
	[SerializeField] private int _amountNeeded;
	[SerializeField] private FolderProvider _paperProvider;
	[SerializeField] private FolderConsumptionController _folderConsumptionController;

	public override bool IsProductionRequirementMet()
	{
		List<Folder> folders = _paperProvider.Resources;
		if (!_folderConsumptionController.IsAvailable || folders == null || folders.Count < _amountNeeded)
		{
			return false;
		}

		return true;
	}

	public override void ConsumeRequirements(Action onConsumedCallback)
	{
		_folderConsumptionController.StartConsumption(_amountNeeded, onConsumedCallback);
	}
}