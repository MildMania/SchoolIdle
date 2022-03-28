using MMFramework_2._0.PhaseSystem.Core.EventListener;

public class MoneyProductionController : ProductionController<MoneyProducer, Money>
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