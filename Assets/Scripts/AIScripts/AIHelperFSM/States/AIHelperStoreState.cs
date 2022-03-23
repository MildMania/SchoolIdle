using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;

public class AIHelperStoreState : State<EState, ETransition>
{
    private ProducerBase _currentProducer;

    protected override EState GetStateID()
    {
        return EState.Store;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _currentProducer = SelectProducer();
        MoveToInteractionPoint(_currentProducer.GetInteractionPoint());
    }

    private ProducerBase SelectProducer()
    {
        List<ProducerBase> producers = new List<ProducerBase>();
        ProducerBase currentProducer = default(ProducerBase);

        foreach (ProducerBase producer in producers)
        {
            // do things here
        }

        return currentProducer;
    }

    private void MoveToInteractionPoint(Vector3 pos)
    {

    }
}
