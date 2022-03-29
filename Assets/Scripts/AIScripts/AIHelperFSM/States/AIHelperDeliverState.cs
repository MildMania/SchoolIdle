using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;

using Pathfinding;

public class AIHelperDeliverState : State<EState, ETransition>
{
    // [SerializeField] private AIHelper _aiHelper;

    [SerializeField] private AIMovementBehaviour _movementBehaviour;
    private IAIInteractable _currentConsumer;

    protected override EState GetStateID()
    {
        return EState.Deliver;
    }

    private IAIInteractable SelectConsumer()
    {
        IAIInteractable currentConsumer = default(IAIInteractable);



        //float maxScore = float.MinValue;

        //foreach (var consumer in _aiHelper.GetConsumers())
        //{
        //    // do things here

        //    float dist = (consumer.GetInteractionPoint() - transform.position).magnitude;

        //    //float score = (1 / dist) * capacity;
        //    float score = (1 / dist);

        //    if (score > maxScore)
        //    {
        //        maxScore = score;
        //        currentConsumer = consumer;
        //    }

        //}

        var list = ConsumerProvider.Instance.GetConsumers(typeof(Paper));
        int indx = Random.Range(0, list.Count - 1);

        currentConsumer = list[indx];

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

        //_carrier.Deliver(_currentConsumer);

        FSM.SetTransition(ETransition.Store);
    }
}
