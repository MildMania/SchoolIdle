using UnityEngine;
using EState = CharacterFSMController.EState;

public class WinState : State<EState, EState>
{
	protected override EState GetStateID()
	{
		return EState.Win;
	}
}