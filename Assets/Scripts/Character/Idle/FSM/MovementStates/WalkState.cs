using System;
using UnityEngine;
using EState = CharacterFSMController.EState;


public class WalkState : State<EState, EState>
{
	[MMSerializedInterface(typeof(IMovementCommander))] [SerializeField]
	private Component _movementCommander = null;

	[SerializeField] private Upgradable _characterUpgradable;
	public IMovementCommander MovementCommander => _movementCommander as IMovementCommander;

	[MMSerializedInterface(typeof(IMovementExecutor))] [SerializeField]
	private Component _movementExecutor = null;

	private float _walkSpeed;
	public IMovementExecutor MovementExecutor => _movementExecutor as IMovementExecutor;

	private void Awake()
	{
		_walkSpeed = GameConfigManager.Instance.GameConfig.WalkSpeed;
		_characterUpgradable.OnUpgradableTrackDataInit += OnUpgradableTrackDataInit;
	}

	private void OnDestroy()
	{
		_characterUpgradable.OnUpgradableTrackDataInit -= OnUpgradableTrackDataInit;
	}

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

	private void OnUpgradableTrackDataInit(UpgradableTrackData upgradableTrackData)
	{
		 _walkSpeed = upgradableTrackData.Attributes["CHARACTER_SPEED"];
		Debug.Log("WALK SPEED : " + _walkSpeed);
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
		MovementExecutor.Move(direction,_walkSpeed);
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