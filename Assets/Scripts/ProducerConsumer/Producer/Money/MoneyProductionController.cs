public class MoneyProductionController : ProductionController<MoneyProducer, Money>
{
	private void OnEnable()
	{
		StartCoroutine(ProduceRoutine(_resource));
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}
}