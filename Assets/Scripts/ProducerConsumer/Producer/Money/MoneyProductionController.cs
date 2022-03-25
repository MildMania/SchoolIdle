using System;
using MMFramework.Tasks.Examples;
using UnityEngine;

public class MoneyProductionController : ProductionController<MoneyProducer, Money>
{
	[SerializeField] private PaperConsumptionController _paperConsumptionController;

	private void Awake()
	{
		_paperConsumptionController.OnUnconsumed += OnUnConsumed;
	}

	private void OnDestroy()
	{
		_paperConsumptionController.OnUnconsumed -= OnUnConsumed;
	}

	private void OnUnConsumed()
	{
		_producer.Produce(_producible);
	}
	
}