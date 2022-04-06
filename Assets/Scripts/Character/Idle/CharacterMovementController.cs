using System;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using Pathfinding;
using UnityEngine;


public class CharacterMovementController : MonoBehaviour,
	IMovementCommander
{
	[SerializeField] private PlayerInputController _inputController = null;
	[SerializeField] private CharacterFSMController _fsmController = null;
	[SerializeField] private float _edgeCheckDistance = 0.2f;

	private Rigidbody _rigidbody;

	public Rigidbody Rigidbody
	{
		get
		{
			if (_rigidbody == null)
				_rigidbody = GetComponent<Rigidbody>();

			return _rigidbody;
		}
	}

	private Vector3 _moveForward = Vector3.zero;
	private Vector3 _moveSideways = Vector3.zero;
	private Vector3 _moveDirection = Vector3.zero;

	private float _temporaryY = 0f;

	private bool _isPrevMoving = false;

	private bool _gameStarted = false;
	public Action<Vector3> OnMoveCommand { get; set; }
	public Action OnMoveCancelledCommand { get; set; }
	public Action<Vector3> OnLookAtCommand { get; set; }


	private void Update()
	{
		if (_gameStarted)
		{
			TryMove();
		}
	}

	[PhaseListener(typeof(GamePhase), true)]
	private void Init()
	{
		_gameStarted = true;
	}
	
	private void TryMove()
	{
		if (_inputController.IsDown)
		{
			UpdateMovementVectors();
			_isPrevMoving = true;
		}
		else if (_isPrevMoving)
		{
			OnMoveCancelledCommand?.Invoke();
			_isPrevMoving = false;
			ResetMovementVectors();
		}

		_temporaryY = _moveDirection.y;
	}

	private void ResetMovementVectors()
	{
		_moveDirection = Vector3.zero;
	}

	private void UpdateMovementVectors()
	{
		_moveForward = Vector3.forward * _inputController.Vertical();
		_moveSideways = Vector3.right * _inputController.Horizontal();

		Vector3 moveDir = (_moveForward + _moveSideways).normalized;

		Quaternion transition =
			Quaternion.LookRotation(-Vector3.up, UnityEngine.Camera.main.transform.forward) *
			Quaternion.Euler(Vector3.right * -90f);

		Vector3 translatedDirection = transition * moveDir;

		_moveDirection = translatedDirection;

		_moveDirection.y = _temporaryY;

		if (_moveDirection != Vector3.zero)
			CheckMovement();
		else
			OnMoveCancelledCommand?.Invoke();

		OnLookAtCommand?.Invoke(translatedDirection);
	}

	private void CheckMovement()
	{
		Vector3 curPosition = Rigidbody.transform.position;
		Vector3 newPosition = curPosition + _moveDirection * _edgeCheckDistance;

		if (AStarExtensions.IsOnGraph(
			newPosition,
			_edgeCheckDistance,
			out Vector3 hitPosition))
		{
			_fsmController.SetTransition(CharacterFSMController.EState.Walk);
			OnMoveCommand?.Invoke(_moveDirection);
		}
	}
}