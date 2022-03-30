using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMFramework.TasksV2;
using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;
using Pathfinding;

public class AIHelperStoreState : State<EState, ETransition>
{
    [SerializeField] private AIHelper _aiHelper;

    [SerializeField] protected AIMovementBehaviour _movementBehaviour;
    [SerializeField] protected MMTaskExecutor _onMovementCompletedTasks;

    protected BaseProducer _currentProducer;

    protected override EState GetStateID()
    {
        return EState.Store;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _currentProducer = SelectProducer();
        MoveToInteractionPoint(_currentProducer.AiInteraction.GetInteractionPoint());

        _aiHelper.CurrentLoadBehaviour.OnCapacityFull += OnCapacityFull;
    }

    protected override void OnExitCustomActions()
    {
        base.OnExitCustomActions();

        _aiHelper.CurrentLoadBehaviour.OnCapacityFull -= OnCapacityFull;
    }

    private BaseProducer SelectProducer()
    {
        float minDist = float.MaxValue;

        BaseProducer currentProducer = default(BaseProducer);
        List<BaseProducer> allProducers = GetProducers();

        foreach (var producer in allProducers)
        {
            float dist = (producer.AiInteraction.GetInteractionPoint() - transform.position).magnitude;

            if (dist < minDist)
                currentProducer = producer;
        }

        return currentProducer;
    }

    protected List<BaseProducer> GetProducers()
    {
        return ProducerProvider.Instance.GetProducers(_aiHelper.Resource.GetType());
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
    }

    private void OnCapacityFull()
    {
        FSM.SetTransition(ETransition.Deliver);
    }
}