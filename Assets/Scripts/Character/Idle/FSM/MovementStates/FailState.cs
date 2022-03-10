using UnityEngine;
using EState = CharacterFSMController.EState;


public class FailState : State<EState, EState>
{
	protected override EState GetStateID()
	{
		return EState.Fail;
	}
}