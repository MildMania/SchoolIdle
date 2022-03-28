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

	private IEnumerator _checkGroundRoutine;
	
	public Action OnIdleStateEnter { get; set; }
	public Action OnIdleStateExit { get; set; }

	protected override EState GetStateID()
	{
		return EState.MovementIdle;
	}


	private void Awake()
	{
		_fovBasedUpgradeAreaDetector.OnDetected += OnUpgradeAreaDetected;
		_fovBasedUpgradeAreaDetector.OnEnded += OnUpgradeAreaEnded;
	}

	private void OnDestroy()
	{
		_fovBasedUpgradeAreaDetector.OnDetected -= OnUpgradeAreaDetected;
		_fovBasedUpgradeAreaDetector.OnEnded -= OnUpgradeAreaEnded;
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