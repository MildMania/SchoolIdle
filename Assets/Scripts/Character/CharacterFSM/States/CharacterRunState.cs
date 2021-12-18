using System;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;
using UnityEngine.Serialization;
using EState = CharacterFSMController.EState;
using ETransition = CharacterFSMController.ETransition;
using MMUtils = MMFramework.Utilities.Utilities;

public class CharacterRunState : State<EState, ETransition>
{
    [SerializeField] private CharacterAnimationController _characterAnimationController = null;
    [SerializeField] private CharacterInputController _characterInputController = null;
    [SerializeField] private BaseCharacterMovementBehaviour _characterMovementBehaviour;

    public float XSwipeAmount { get; private set; } = 0;
    private float _platformWidth;

    #region Events

    public Action<Vector3> OnCharacterMoved { get; set; }

    #endregion

    protected override EState GetStateID()
    {
        return EState.Run;
    }

    public override void OnEnterCustomActions()
    {
        _characterAnimationController.PlayAnimation(ECharacterAnimation.Run);

        base.OnEnterCustomActions();
    }

    private void Awake()
    {
        _platformWidth = Math.Abs(LevelBoundaryProvider.Instance.GetLeftBoundary().x -
                                  LevelBoundaryProvider.Instance.GetRightBoundary().x);
        SubscribeToEvents();
    }

    private void Update()
    {
        TryMove();
    }

    private void OnDestroy()
    {
        UnsubscribeFromEvents();
    }

    [PhaseListener(typeof(GamePhase), true)]
    private void SubscribeToEvents()
    {
        RegisterToInputController();
    }

    [PhaseListener(typeof(GamePhase), false)]
    private void UnsubscribeFromEvents()
    {
        UnregisterFromInputController();
    }


    #region Sideways Input

    private void RegisterToInputController()
    {
        _characterInputController.OnCharacterInputStarted += OnInputStarted;
        _characterInputController.OnCharacterInputPerformed += OnInputPerformed;
        _characterInputController.OnCharacterInputCancelled += OnInputCancelled;
    }

    private void UnregisterFromInputController()
    {
        _characterInputController.OnCharacterInputStarted -= OnInputStarted;
        _characterInputController.OnCharacterInputPerformed -= OnInputPerformed;
        _characterInputController.OnCharacterInputCancelled -= OnInputCancelled;
    }

    private void OnInputStarted(Vector2 delta)
    {
        FSM.SetTransition(ETransition.Run);
    }

    private void OnInputPerformed(Vector2 delta)
    {
        if (!FSM.GetCurStateID().Equals(GetStateID()))
        {
            FSM.SetTransition(ETransition.Run);
        }

        XSwipeAmount += delta.x * _platformWidth;

        XSwipeAmount = Mathf.Clamp(XSwipeAmount,
            LevelBoundaryProvider.Instance.GetLeftBoundary().x,
            LevelBoundaryProvider.Instance.GetRightBoundary().x);
    }

    private void OnInputCancelled(Vector2 delta)
    {
    }

    #endregion

    private bool TryMove()
    {
        if (!CanMove())
        {
            return false;
        }

        _characterMovementBehaviour.Move(XSwipeAmount);

        return true;
    }

    private bool CanMove()
    {
        return FSM.GetCurStateID().Equals(GetStateID());
    }
}