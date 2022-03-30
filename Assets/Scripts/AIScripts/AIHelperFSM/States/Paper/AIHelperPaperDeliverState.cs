using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHelperPaperDeliverState : AIHelperDeliverState
{
    [SerializeField] private PaperDelivererAIHelper _paperDelivererAiHelper;

    protected override List<IAIInteractable> GetConsumers()
    {
        return ConsumerProvider.Instance.GetConsumers(typeof(Paper));
    }

    protected override void OnDeliverStateCustomActions()
    {
        _paperDelivererAiHelper.PaperLoadBehaviour.OnCapacityEmpty += OnCapacityEmpty;
    }

    protected override void OnExitCustomActions()
    {
        _paperDelivererAiHelper.PaperLoadBehaviour.OnCapacityEmpty -= OnCapacityEmpty;
    }

    private void OnCapacityEmpty()
    {
        FSM.SetTransition(AIHelperFSMController.ETransition.Store);
    }
}
