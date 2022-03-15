using System.Collections.Generic;
using EState = ConsumerFSMController.EState;
using ST =
    StateTransition<ConsumerFSMController.EState,
        ConsumerFSMController.EState>;


public class ConsumerFSM : MMFSM<EState, EState>
{
    protected override EState GetEnteranceStateID()
    {
        return EState.Idle;
    }

    protected override Dictionary<StateTransition<EState, EState>, EState> GetTransitionDict()
    {
        return new Dictionary<ST, EState>
        {
            {new ST(EState.Idle, EState.Consume), EState.Consume},

            {new ST(EState.Consume, EState.Idle), EState.Idle},
        };
    }
}