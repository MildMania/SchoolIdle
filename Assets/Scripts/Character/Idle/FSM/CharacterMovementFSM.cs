using System.Collections.Generic;
using EState = CharacterFSMController.EState;
using ST =
	StateTransition<CharacterFSMController.EState,
		CharacterFSMController.EState>;


public class CharacterMovementFSM : MMFSM<EState, EState>
{
	protected override EState GetEnteranceStateID()
	{
		return EState.MovementIdle;
	}

	protected override Dictionary<StateTransition<EState, EState>, EState> GetTransitionDict()
	{
		return new Dictionary<ST, EState>
		{
			{new ST(EState.MovementIdle, EState.Walk), EState.Walk},

			{new ST(EState.Walk, EState.MovementIdle), EState.MovementIdle},
			
			{new ST(EState.Walk, EState.Fail), EState.Fail},
			
			{new ST(EState.Walk, EState.Win), EState.Win}
		};
	}
}