using MMFramework_2._0.PhaseSystem.Core.EventListener;

public class PaperProductionController : ProductionController<PaperProducer, Paper>
{
	[PhaseListener(typeof(GamePhase), true)]
	public void OnGamePhaseStarted()
	{
		StartCoroutine(ProduceRoutine(_producible));
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}
	
}