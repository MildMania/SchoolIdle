using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIHelperPaperDeliverState : AIHelperDeliverState
{
    [SerializeField] private PaperDelivererAIHelper _paperDelivererAiHelper;

    protected override IAIInteractable SelectConsumer()
    {
        IAIInteractable currentConsumer = default(IAIInteractable);

        var list = ConsumerProvider.Instance.GetConsumers(typeof(Paper));
        int indx = Random.Range(0, list.Count - 1);

        currentConsumer = list[indx];

        return currentConsumer;
    }

    protected override void OnDeliverStateCustomActions()
    {
        _paperDelivererAiHelper.PaperLoadBehaviour.OnCapacityEmpty += OnCapacityEmpty;
    }

    private void OnCapacityEmpty()
    {
        FSM.SetTransition(AIHelperFSMController.ETransition.Store);
    }
}
