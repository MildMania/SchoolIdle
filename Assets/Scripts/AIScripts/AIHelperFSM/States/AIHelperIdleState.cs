using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using MMFramework.Utilities;
using UnityEngine;

public class AIHelperIdleState : State<EState, ETransition>
{
    [SerializeField] private float _delay;
    protected override EState GetStateID()
    {
        return EState.Idle;
    }

    public override void OnEnterCustomActions()
    {
        base.OnEnterCustomActions();


    }

    [PhaseListener(typeof(GamePhase), true)]
    public void OnGamePhaseStarted()
    {
        CoroutineRunner.Instance.WaitForSeconds(_delay, () =>
        {
            FSM.SetTransition(ETransition.Store);
        });
    }

}