using System;

public class PaperConsumptionController : ConsumptionController<PaperConsumer,Paper>
{
	private void Awake()
	{
		StartCoroutine(ConsumeRoutine(_consumable,_consumer.Deliverers));
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}
}