using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;

using Pathfinding;

public class AIHelperDeliverState : State<EState, ETransition>
{
    [SerializeField] private Carrier_Test _carrier;
    [SerializeField] private List<Consumer_Test> _consumerList;

    [SerializeField] private AIMovementBehaviour _movementBehaviour;
    private Consumer_Test _currentConsumer;

    protected override EState GetStateID()
    {
        return EState.Deliver;
    }

    private Consumer_Test SelectConsumer()
    {
        Consumer_Test currentConsumer = default(Consumer_Test);

        float minCost = float.MaxValue;

        foreach (var consumer in _consumerList)
        {
            // do things here

            float dist = (consumer.transform.position - transform.position).magnitude;
            float capacity = consumer.Capacity;

            float cost = dist * (1 / capacity);

            if(cost <= minCost)
            {
                minCost = cost;
                currentConsumer = consumer;
            }

        }

        return currentConsumer;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _currentConsumer = SelectConsumer();
        MoveToInteractionPoint(_currentConsumer.GetInteractionPoint());
    }

    private void MoveToInteractionPoint(Vector3 pos)
    {
        _movementBehaviour.MoveDestination(pos, OnPathCompleted);
    }

    private void OnPathCompleted()
    {
        // TODO : start deliver routine

        _carrier.Deliver(_currentConsumer);

        FSM.SetTransition(ETransition.Store);
    }
}
