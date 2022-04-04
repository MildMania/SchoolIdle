using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;
using Random = UnityEngine.Random;

using DG.Tweening;

public class AIHelperDeliverState : State<EState, ETransition>
{
    [SerializeField] private AIHelper _aiHelper;
    [SerializeField] private AIMovementBehaviour _movementBehaviour;
    [SerializeField] private HelperAnimationController _helperAnimationController;
    [SerializeField] private float _pollDelay = 5;

    private BaseConsumer _currentConsumer;
    private WaitForSeconds _pollWfs;

    private Vector3 _lastPos;

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

        int index = Random.Range(0, list.Count);
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
        _aiHelper.CurrentUnloadBehaviour.Deactivate();
        _aiHelper.CurrentLoadBehaviour.Deactivate();
    }

    private void MoveToInteractionPoint(Vector3 pos)
    {
        _lastPos = pos;
        _movementBehaviour.MoveDestination(pos, OnPathCompleted);
    }

    private void OnPathCompleted()
    {
        _aiHelper.CurrentLoadBehaviour.Activate();

        Vector3 dir = (new Vector3(_aiHelper.transform.position.x, _aiHelper.transform.position.y, _lastPos.z) - _aiHelper.transform.position).normalized;

        _aiHelper.transform.DORotateQuaternion(Quaternion.LookRotation(dir), 0.1f).OnComplete(() =>
        {
            _aiHelper.CurrentUnloadBehaviour.Activate();
        });
        // TODO: we need coroutine rather than path completed event
    }

    private void OnCapacityEmpty()
    {
        FSM.SetTransition(AIHelperFSMController.ETransition.Store);
    }
}