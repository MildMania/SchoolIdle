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
            //{ new ST(EState.Idle, ETransition.Run), EState.Run },
            //{ new ST(EState.Run, ETransition.Idle), EState.Idle },
            //{ new ST(EState.Run, ETransition.Fall), EState.Fall },
            //{ new ST(EState.Fall, ETransition.Fail), EState.Fail },
            //{ new ST(EState.Run, ETransition.Fail), EState.Fail },
            //{ new ST(EState.Run, ETransition.EndGame), EState.EndGame },
            //{ new ST(EState.EndGame, ETransition.Win), EState.Win },
        };
    }
}