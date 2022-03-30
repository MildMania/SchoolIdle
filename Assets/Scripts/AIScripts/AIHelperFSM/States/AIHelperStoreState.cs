using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMFramework.TasksV2;
using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;
using Pathfinding;

public abstract class AIHelperStoreState : State<EState, ETransition>
{
    // [SerializeField] private AIHelper _aiHelper;

    [SerializeField] protected AIMovementBehaviour _movementBehaviour;
    [SerializeField] protected MMTaskExecutor _onMovementCompletedTasks;

    protected IAIInteractable _currentProducer;

    protected override EState GetStateID()
    {
        return EState.Store;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _currentProducer = SelectProducer();
        MoveToInteractionPoint(_currentProducer.GetInteractionPoint());

        OnStoreStateCustomActions();
    }

    protected abstract void OnStoreStateCustomActions();

    private IAIInteractable SelectProducer()
    {
        float minDist = float.MaxValue;

        IAIInteractable currentProducer = default(IAIInteractable);
        List<IAIInteractable> allProducers = GetProducers();

        foreach (var producer in allProducers)
        {
            float dist = (producer.GetInteractionPoint() - transform.position).magnitude;

            if (dist < minDist)
                currentProducer = producer;
        }

        return currentProducer;
    }

    protected abstract List<IAIInteractable> GetProducers();

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
}