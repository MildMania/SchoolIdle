using System;
using Pathfinding;
using System.Collections;
using UnityEngine;
using EState = CharacterFSMController.EState;


public class MovementIdleState : State<EState, EState>
{
	[SerializeField] private Seeker _seeker = null;
	[SerializeField] private Rigidbody _rigidbody = null;

	[SerializeField] private FovBasedUpgradeAreaDetector _fovBasedUpgradeAreaDetector;

	[SerializeField] private CharacterAnimationController _characterAnimationController;
	[SerializeField] private Deliverer _deliverer;

	private IEnumerator _checkGroundRoutine;
	
	public Action OnIdleStateEnter { get; set; }
	public Action OnIdleStateExit { get; set; }

	protected override EState GetStateID()
	{
		return EState.MovementIdle;
	}

	public bool IsOnIdleState()
	{
		return FSM.GetCurStateID() == EState.MovementIdle;
	}


	private void Awake()
	{
		_fovBasedUpgradeAreaDetector.OnDetected += OnUpgradeAreaDetected;
		_fovBasedUpgradeAreaDetector.OnEnded += OnUpgradeAreaEnded;

		_deliverer.OnContainerEmpty += OnContainerEmpty;
		
	}

	private void OnContainerEmpty(bool isContainerEmpty)
	{
		_characterAnimationController.Animator.SetLayerWeight(1, isContainerEmpty ? 0f : 1f);
	}

	private void OnDestroy()
	{
		_fovBasedUpgradeAreaDetector.OnDetected -= OnUpgradeAreaDetected;
		_fovBasedUpgradeAreaDetector.OnEnded -= OnUpgradeAreaEnded;
		
		_deliverer.OnContainerEmpty -= OnContainerEmpty;
	}

	private void OnUpgradeAreaDetected(UpgradeArea upgradeArea)
	{
		upgradeArea.ShowWidget();
	}
	
	private void OnUpgradeAreaEnded(UpgradeArea upgradeArea)
	{
		upgradeArea.HideWidget();
	}

	public override void OnEnterCustomActions()
	{
		
		OnIdleStateEnter?.Invoke();
		
		_checkGroundRoutine = CheckGroundProgress();
		StartCoroutine(_checkGroundRoutine);

		base.OnEnterCustomActions();
		
		_characterAnimationController.PlayAnimation(ECharacterAnimation.Idle);
	}

	protected override void OnExitCustomActions()
	{
		OnIdleStateExit?.Invoke();

		if (_checkGroundRoutine != null)
			StopCoroutine(_checkGroundRoutine);

		base.OnExitCustomActions();
	}

	private IEnumerator CheckGroundProgress()
	{
		while (true)
		{
			if (_seeker.IsOnGraph(distTreshold: 0.1f, out Vector3 position))
			{
				_rigidbody.transform.position = position;
				
			}


			yield return null;
		}
	}
}