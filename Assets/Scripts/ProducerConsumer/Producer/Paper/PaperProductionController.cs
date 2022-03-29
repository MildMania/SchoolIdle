using System;
using MMFramework_2._0.PhaseSystem.Core.EventListener;

public class PaperProductionController : ProductionController<PaperProducer, Paper>
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