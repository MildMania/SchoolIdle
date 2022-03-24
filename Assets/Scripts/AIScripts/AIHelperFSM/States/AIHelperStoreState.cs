using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.TasksV2;

using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;

using Pathfinding;

public class AIHelperStoreState : State<EState, ETransition>
{
    [SerializeField] private Carrier_Test _carrier;
    [SerializeField] private List<Producer_Test> _producerList;

    [SerializeField] private AIMovementBehaviour _movementBehaviour;

    [SerializeField] private MMTaskExecutor _onMovementCompletedTasks;

    private Producer_Test _currentProducer;

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

    private Producer_Test SelectProducer()
    {
        Producer_Test currentProducer = default(Producer_Test);

        float minDist = float.MaxValue;

        foreach (var producer in _producerList)
        {
            float dist = (producer.transform.position - transform.position).magnitude;

            if (dist < minDist)
                currentProducer = producer;
        }

        return currentProducer;
    }

    private void MoveToInteractionPoint(Vector3 pos)
    {
        _movementBehaviour.MoveDestination(pos, OnPathCompleted);
    }

    private void OnPathCompleted()
    {
        // TODO : start store routine

        if (_onMovementCompletedTasks != null)
            _onMovementCompletedTasks.Execute(this);

        _carrier.Store(_currentProducer); // this might be change little bit

        FSM.SetTransition(ETransition.Deliver);
    }
}
