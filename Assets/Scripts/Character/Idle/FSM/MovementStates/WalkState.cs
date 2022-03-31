using System;
using System.Collections.Generic;
using UnityEngine;
using EState = CharacterFSMController.EState;


public class WalkState : State<EState, EState>
{
	[MMSerializedInterface(typeof(IMovementCommander))] [SerializeField]
	private Component _movementCommander = null;

	[SerializeField] private EAttributeCategory _attributeCategory;
	[SerializeField] private EUpgradable _speedUpgradableType;
	
	private Upgradable _characterSpeedUpgradable;
	public IMovementCommander MovementCommander => _movementCommander as IMovementCommander;

	[MMSerializedInterface(typeof(IMovementExecutor))] [SerializeField]
	private Component _movementExecutor = null;

	private float _walkSpeed;
	public IMovementExecutor MovementExecutor => _movementExecutor as IMovementExecutor;

	[SerializeField] private CharacterAnimationController _characterAnimationController;

	

	private void Start()
	{
		_characterSpeedUpgradable = HelperUpgradableManager.Instance.GetUpgradable(_attributeCategory, _speedUpgradableType);

		_walkSpeed = GameConfigManager.Instance.GetAttributeUpgradeValue(_attributeCategory, _characterSpeedUpgradable.UpgradableTrackData);

		_characterSpeedUpgradable.OnUpgraded += OnSpeedUpgraded;
	}

	private void OnDestroy()
	{
		_characterSpeedUpgradable.OnUpgraded -= OnSpeedUpgraded;
	}

	public override void OnEnterCustomActions()
	{
		RegisterToMovementCommander();
		base.OnEnterCustomActions();
		
		_characterAnimationController.PlayAnimation(ECharacterAnimation.Walk);
	}

	protected override void OnExitCustomActions()
	{
		UnregisterFromMovementCommander();
		base.OnExitCustomActions();
	}

	private void OnSpeedUpgraded(UpgradableTrackData upgradableTrackData)
	{
		float value = GameConfigManager.Instance.GetAttributeUpgradeValue(EAttributeCategory.CHARACTER, upgradableTrackData);

		_walkSpeed = value;
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