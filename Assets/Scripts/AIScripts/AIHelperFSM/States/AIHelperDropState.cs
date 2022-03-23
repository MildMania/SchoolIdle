using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;

public class AIHelperDropState : State<EState, ETransition>
{
    private ConsumerBase _currentConsumer;

    protected override EState GetStateID()
    {
        return EState.Drop;
    }

    private ConsumerBase SelectConsumer()
    {
        List<ConsumerBase> consumers = new List<ConsumerBase>();
        ConsumerBase currentComsumer = default(ConsumerBase);

        foreach (ConsumerBase consumer in consumers)
        {
            // do things here
        }

        return currentComsumer;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _currentConsumer = SelectConsumer();
        MoveToInteractionPoint(_currentConsumer.GetInteractionPoint());
    }

    private void MoveToInteractionPoint(Vector3 pos)
    {

    }
}
