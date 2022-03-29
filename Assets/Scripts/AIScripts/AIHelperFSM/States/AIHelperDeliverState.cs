using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;

using Pathfinding;

public abstract class AIHelperDeliverState : State<EState, ETransition>
{

    [SerializeField] private AIMovementBehaviour _movementBehaviour;
    private IAIInteractable _currentConsumer;

    protected override EState GetStateID()
    {
        return EState.Deliver;
    }

    protected abstract IAIInteractable SelectConsumer();
    protected abstract void OnDeliverStateCustomActions();

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();

        _currentConsumer = SelectConsumer();
        MoveToInteractionPoint(_currentConsumer.GetInteractionPoint());

        OnDeliverStateCustomActions();
    }

    private void MoveToInteractionPoint(Vector3 pos)
    {
        _movementBehaviour.MoveDestination(pos, OnPathCompleted);
    }

    private void OnPathCompleted()
    {
        
    }
}
