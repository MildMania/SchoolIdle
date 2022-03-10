using System.Linq;
using MildMania.IdleArcade.Player;
using ProgressionSystem;
using UnityEngine;
using EState = CharacterFSMController.EState;

public class LevelFailure_CharacterState : LevelFailureBase
{
    [SerializeField] private CharacterMovementFSM _characterFSM = null;

    [SerializeField] private EState[] _failureStates = default;

    private bool _isInFailureState = false;
    
    public override bool CheckProgression()
    {
        return _isInFailureState;
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
        if (_failureStates.Contains(state))
        {
            _isInFailureState = true;

            return;
        }

        _isInFailureState = false;
    }
}
