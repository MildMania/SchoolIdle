using System.Linq;
using MildMania.IdleArcade.Player;
using ProgressionSystem;
using UnityEngine;
using EState = CharacterFSMController.EState;

public class LevelSuccess_CharacterState : LevelSuccessBase
{
	[SerializeField] private CharacterMovementFSM _characterFSM = null;

	[SerializeField] private EState[] _successStates = default;

	private bool _isInSuccessState = false;

	public override bool CheckProgression()
	{
		return _isInSuccessState;
	}

	private void Awake()
	{
		RegisterToCharacterFSM();
	}

	private void OnDestroy()
	{
		UnregisterFromCharacterFSM();
	}

	private void RegisterToCharacterFSM()
	{
		_characterFSM.AddOnStateEntered(OnStateEntered);
	}

	private void UnregisterFromCharacterFSM()
	{
		_characterFSM.RemoveOnStateEntered(OnStateEntered);
	}

	private void OnStateEntered(EState state)
	{
		if (_successStates.Contains(state))
		{
			_isInSuccessState = true;

			return;
		}

		_isInSuccessState = false;
	}
}