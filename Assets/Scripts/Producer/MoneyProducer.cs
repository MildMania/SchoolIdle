using UnityEngine;


public class MoneyProducer : ProducerBase
{
	[SerializeField] private ConsumerBase _consumerBase;

	protected override void OnAwakeCustomActions()
	{
		base.OnAwakeCustomActions();

		Debug.Log("OnAwakeCustomActions");
		_consumerBase.OnConsumerStopped += OnConsumerStopped;
	}

	protected override void OnDestroyCustomActions()
	{
		base.OnDestroyCustomActions();
		_consumerBase.OnConsumerStopped -= OnConsumerStopped;
	}

	private void OnConsumerStopped()
	{
		StopProduce();
	}

	protected override void OnDroppedCustomActions()
	{
		base.OnDroppedCustomActions();
		Debug.Log("OnDroppedCustomActions");

		if (_produceRoutine == null)
		{
			StartProduce();
		}
	}
}