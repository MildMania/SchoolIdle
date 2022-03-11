using UnityEngine;
using EState = CharacterFSMController.EState;


public class WalkState : State<EState, EState>
{
	[MMSerializedInterface(typeof(IMovementCommander))] [SerializeField]
	private Component _movementCommander = null;

	public IMovementCommander MovementCommander => _movementCommander as IMovementCommander;

	[MMSerializedInterface(typeof(IMovementExecutor))] [SerializeField]
	private Component _movementExecutor = null;

	public IMovementExecutor MovementExecutor => _movementExecutor as IMovementExecutor;
	

	public override void OnEnterCustomActions()
	{
		RegisterToMovementCommander();
		base.OnEnterCustomActions();
	}

	protected override void OnExitCustomActions()
	{
		UnregisterFromMovementCommander();
		base.OnExitCustomActions();
	}

	private void RegisterToMovementCommander()
	{
		MovementCommander.OnMoveCommand += OnMoveCommand;
		MovementCommander.OnMoveCancelledCommand += OnMoveCancelledCommand;
		MovementCommander.OnLookAtCommand += OnLookAtCommand;
	}

	private void UnregisterFromMovementCommander()
	{
		MovementCommander.OnMoveCommand -= OnMoveCommand;
		MovementCommander.OnMoveCancelledCommand -= OnMoveCancelledCommand;
		MovementCommander.OnLookAtCommand -= OnLookAtCommand;
	}

	private void OnMoveCommand(Vector3 direction)
	{
		Debug.Log("OnMoveCommand CALLED!");
		MovementExecutor.Move(direction, GameConfigManager.Instance.GameConfig.WalkSpeed);
	}

	private void OnMoveCancelledCommand()
	{
		MovementExecutor.Stop();
		FSM.FSMController.SetTransition(EState.MovementIdle);
	}

	private void OnLookAtCommand(Vector3 direction)
	{
		Vector3 targetPos = transform.position + direction;

		MovementExecutor.LookAt(targetPos);
	}

	protected override EState GetStateID()
	{
		return EState.Walk;
	}
}