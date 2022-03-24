using System.Collections.Generic;
using UnityEngine;
using EState = AIHelperFSMController.EState;
using ETransition = AIHelperFSMController.ETransition;
using ST = StateTransition<AIHelperFSMController.EState, AIHelperFSMController.ETransition>;

public class AIHelperFSM : MMFSM<EState, ETransition>
{
    [SerializeField] private EState _initialState = EState.Idle;

    protected override EState GetEnteranceStateID()
    {
        return _initialState;
    }

    protected override Dictionary<ST, EState> GetTransitionDict()
    {
        return new Dictionary<ST, EState>
        {
            { new ST(EState.Idle, ETransition.Store), EState.Store},
            { new ST(EState.Idle, ETransition.Deliver), EState.Deliver},
            { new ST(EState.Store, ETransition.Deliver), EState.Deliver},
            { new ST(EState.Deliver, ETransition.Store), EState.Store},
        };
    }
}