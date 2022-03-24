using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumptionController<TConsumer, TConsumable> : MonoBehaviour where TConsumer : BaseConsumer<TConsumable>
	where TConsumable : IConsumable
{
	[SerializeField] protected TConsumer _consumer;
	[SerializeField] protected TConsumable _consumable;
	[SerializeField] private float _consumptionDelay;
	
	protected IEnumerator ConsumeRoutine(TConsumable consumable, List<Deliverer> deliverers)
	{
		float currentTime = 0;

		while (true)
		{
			currentTime += Time.deltaTime;

			if (currentTime > _consumptionDelay && deliverers.Count > 0)
			{
				_consumer.Consume(consumable);
				currentTime = 0;
			}

			yield return null;
		}
	}
}