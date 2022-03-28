
using MMFramework_2._0.PhaseSystem.Core.EventListener;

public class FolderProductionController : ProductionController<FolderProducer, Folder>
{
	[PhaseListener(typeof(GamePhase), true)]
	public void OnGamePhaseStarted()
	{
		StartCoroutine(ProduceRoutine(_resource));
	}

	private void OnDestroy()
	{
		StopAllCoroutines();
	}
}