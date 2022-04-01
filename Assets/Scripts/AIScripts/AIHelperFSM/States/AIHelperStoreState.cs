using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MMFramework.TasksV2;
using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;
using Pathfinding;

using DG.Tweening;

public class AIHelperStoreState : State<EState, ETransition>
{
    [SerializeField] private AIHelper _aiHelper;

    [SerializeField] protected AIMovementBehaviour _movementBehaviour;
    [SerializeField] protected MMTaskExecutor _onMovementCompletedTasks;
    [SerializeField] private HelperAnimationController _helperAnimationController;
    [SerializeField] private float _pollDelay = 5;

    private BaseProducer _currentProducer;
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
        return EState.Store;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();
        StartCoroutine(SelectProducerRoutine());
    }

    private IEnumerator SelectProducerRoutine()
    {
        _currentProducer = SelectProducer();

        while (_currentProducer == null)
        {
            yield return _pollWfs;
            _currentProducer = SelectProducer();
        }

        MoveToStorePoint();
    }

    private void MoveToStorePoint()
    {
        MoveToInteractionPoint(_currentProducer.AiInteraction.GetInteractionPoint());
        _helperAnimationController.PlayAnimation(EHelperAnimation.Walk);
        _aiHelper.CurrentLoadBehaviour.OnCapacityFull += OnCapacityFull;
    }

    protected override void OnExitCustomActions()
    {
        _aiHelper.CurrentLoadBehaviour.OnCapacityFull -= OnCapacityFull;

        _aiHelper.CurrentLoadBehaviour.Deactivate();
    }

    private BaseProducer SelectProducer()
    {
        // float minDist = float.MaxValue;

        List<BaseProducer> allProducers = GetProducers();

        if (allProducers == null || allProducers.Count == 0)
        {
            return null;
        }

        int index = Random.Range(0, allProducers.Count - 1);
        var currentProducer = allProducers[index];

        // foreach (var producer in allProducers)
        // {
        //     float dist = (producer.AiInteraction.GetInteractionPoint() - transform.position).magnitude;
        //
        //     if (dist < minDist)
        //     {
        //         currentProducer = producer;
        //         minDist = dist;
        //     }
        // }

        return currentProducer;
    }

    protected List<BaseProducer> GetProducers()
    {
        return ProducerProvider.Instance.GetProducers(_aiHelper.Resource.GetType());
    }

    private void MoveToInteractionPoint(Vector3 pos)
    {
        _lastPos = pos;
        _movementBehaviour.MoveDestination(pos, OnPathCompleted);
    }

    private void OnPathCompleted()
    {
        Vector3 dir = (new Vector3(_aiHelper.transform.position.x, _aiHelper.transform.position.y, _lastPos.z) - _aiHelper.transform.position).normalized;

        _aiHelper.transform.DORotateQuaternion(Quaternion.LookRotation(dir), 0.1f);

        _aiHelper.CurrentLoadBehaviour.Activate();

        if (_onMovementCompletedTasks != null)
            _onMovementCompletedTasks.Execute(this);
    }

    private void OnCapacityFull()
    {
        FSM.SetTransition(ETransition.Deliver);
    }
}