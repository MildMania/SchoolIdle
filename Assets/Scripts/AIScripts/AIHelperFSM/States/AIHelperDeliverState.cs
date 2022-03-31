using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;
using Random = UnityEngine.Random;

public class AIHelperDeliverState : State<EState, ETransition>
{
    [SerializeField] private AIHelper _aiHelper;
    [SerializeField] private AIMovementBehaviour _movementBehaviour;
    [SerializeField] private HelperAnimationController _helperAnimationController;
    [SerializeField] private float _pollDelay = 5;

    private BaseConsumer _currentConsumer;
    private WaitForSeconds _pollWfs;

    private void Awake()
    {
        _pollWfs = new WaitForSeconds(_pollDelay);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    protected override EState GetStateID()
    {
        return EState.Deliver;
    }


    private BaseConsumer SelectConsumer()
    {
        var list = GetConsumers();
        if (list == null || list.Count == 0)
        {
            return null;
        }

        int index = Random.Range(0, list.Count - 1);
        var currentConsumer = list[index];
        return currentConsumer;
    }

    protected List<BaseConsumer> GetConsumers()
    {
        return ConsumerProvider.Instance.GetConsumers(_aiHelper.Resource.GetType());
    }

    public override void OnEnterCustomActions()
    {
        StartCoroutine(SelectConsumerRoutine());
    }

    private IEnumerator SelectConsumerRoutine()
    {
        _currentConsumer = SelectConsumer();

        while (_currentConsumer == null)
        {
            yield return _pollWfs;
            _currentConsumer = SelectConsumer();
        }

        MoveToDeliveryPoint();
    }

    private void MoveToDeliveryPoint()
    {
        MoveToInteractionPoint(_currentConsumer.AiInteraction.GetInteractionPoint());
        _helperAnimationController.PlayAnimation(EHelperAnimation.Walk);
        _aiHelper.CurrentLoadBehaviour.OnCapacityEmpty += OnCapacityEmpty;
    }

    protected override void OnExitCustomActions()
    {
        _aiHelper.CurrentLoadBehaviour.OnCapacityEmpty -= OnCapacityEmpty;
    }

    private void MoveToInteractionPoint(Vector3 pos)
    {
        _movementBehaviour.MoveDestination(pos, OnPathCompleted);
    }

    private void OnPathCompleted()
    {
    }

    private void OnCapacityEmpty()
    {
        FSM.SetTransition(AIHelperFSMController.ETransition.Store);
    }
}