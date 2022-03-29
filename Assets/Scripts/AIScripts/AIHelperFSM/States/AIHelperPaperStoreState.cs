using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;

public class AIHelperPaperStoreState : AIHelperStoreState
{
    [SerializeField] private PaperDelivererAIHelper _paperDelivererAiHelper;

    protected override IAIInteractable SelectProducer()
    {
        float minDist = float.MaxValue;

        IAIInteractable currentProducer = default(IAIInteractable);
        List<IAIInteractable> allProducers = ProducerProvider.Instance.GetProducers(typeof(Paper));

        foreach (var producer in allProducers)
        {
            float dist = (producer.GetInteractionPoint() - transform.position).magnitude;

            if (dist < minDist)
                currentProducer = producer;
        }

        return currentProducer;
    }

    protected override void OnStoreStateCustomActions()
    {
        _paperDelivererAiHelper.PaperLoadBehaviour.OnCapacityFull += OnCapacityFull;
    }

    protected override void OnExitCustomActions()
    {
        base.OnExitCustomActions();

        _paperDelivererAiHelper.PaperLoadBehaviour.OnCapacityFull -= OnCapacityFull;
    }

    private void OnCapacityFull()
    {
        FSM.SetTransition(ETransition.Deliver);
    }
}
