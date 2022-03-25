using System;
using System.Collections;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;

public class ConsumptionController<TConsumer, TConsumable> : MonoBehaviour where TConsumer : BaseConsumer<TConsumable>
	where TConsumable : IConsumable
{
	[SerializeField] protected TConsumer _consumer;
	[SerializeField] protected TConsumable _consumable;
	[SerializeField] private float _consumptionDelay;

	public Action OnUnconsumed { get; set; }
	
	[PhaseListener(typeof(GamePhase), true)]
	public void OnGamePhaseStarted()
	{
		StartCoroutine(UnconsumeRoutine());
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}

	private IEnumerator UnconsumeRoutine()
	{
		while (true)
		{
			if (_consumer.Consumables.Count == 0 )
			{
				yield return null;
				continue;
			}
			
			yield return new WaitForSeconds(_consumptionDelay);
			_consumer.UnconsumeLast();
			OnUnconsumed?.Invoke();
		}
	}
}