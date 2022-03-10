using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInputController : MonoBehaviour, IInputReceiver
{
	#region Input Data

	public abstract class InputDataBase
	{
		public InputDataBase()
		{
		}
	}

	public class InputDownData : InputDataBase
	{
		public Vector2 FingerPos { get; }

		public InputDownData(Vector2 fingerPos) : base()
		{
			FingerPos = fingerPos;
		}
	}

	public class InputDragData : InputDataBase
	{
		public Vector2 DistanceToStart { get; private set; }

		public InputDragData(Vector2 distanceToStart) : base()
		{
			DistanceToStart = distanceToStart;
		}
	}

	public class InputUpData : InputDataBase
	{
		public Vector3 InputVector { get; private set; }

		public InputUpData(Vector3 inputVector) : base()
		{
			InputVector = inputVector;
		}
	}

	#endregion

	public List<InputTransmitter> AttachedInputTransmitterList { get; set; }
	public Dictionary<Type, InputTransmitter.EventDelegate> Delegates { get; set; }
	public Dictionary<Delegate, InputTransmitter.EventDelegate> DelegateLookUp { get; set; }

	public bool IsDown { get; private set; }
	public float horizontalSens = 3f;
	public float verticalSens = 3f;

	public float distanceFromStart { get; private set; }

	/// <summary>
	/// how much of the maximum force will the controller apply
	/// </summary>
	[HideInInspector] public float distanceMultiplier = 1f;

	protected float dragDistance = 0f;

	/// <summary>
	/// The distance when distanceMultiplier reaches it's minimum value
	/// </summary>
	[Range(0, 400)] public float minMovementValueThreshold = 0f;

	public bool IsMoving => IsDown;

	public virtual bool IsAiming => false;

	public virtual bool IsAutoShooting => !IsDown;

	/// <summary>
	/// Amount of movement relative to start position and sensitivity
	/// </summary>
	public virtual Vector3 InputVector => _inputVector;

	private Vector2 _startPosition;
	private Vector3 _inputVector;

	private bool _canMove = false;

	public bool invert = true;


	public float SLOWSPEED = 0.35f;
	public float MEDIUMSPEED = 0.7f;
	public float MAXSPEED = 1f;

	public float MAXDISTANCE = 300f;

	public float MOVETRESHOLD = 50f;
	public float SLOWTRESHOLD = 75f;
	public float MEDTRESHOLD = 125f;

	#region Events

	public Action<InputDownData> OnDown { get; set; }
	public Action<InputDragData> OnDragged { get; set; }
	public Action<InputUpData> OnUp { get; set; }

	#endregion

	public float Horizontal()
	{
		return _inputVector.x;
	}

	public float Vertical()
	{
		return _inputVector.z;
	}

	private void Awake()
	{
		Init();

		RegisterToInputReceiver();
	}

	private void OnDestroy()
	{
		UnregisterFromInputReceiver();
	}

	private void Init()
	{
		_inputVector = Vector3.zero;
	}


	private void RegisterToInputReceiver()
	{
		this.AddInputListener<Input_WI_OnFingerDown>(OnFingerDown);
		this.AddInputListener<Input_WI_OnFingerUp>(OnFingerUp);
		this.AddInputListener<Input_WI_OnDragMove>(OnDragMove);
	}

	private void UnregisterFromInputReceiver()
	{
		this.RemoveInputListener<Input_WI_OnFingerDown>(OnFingerDown);
		this.RemoveInputListener<Input_WI_OnFingerUp>(OnFingerUp);
		this.RemoveInputListener<Input_WI_OnDragMove>(OnDragMove);
	}

	private void OnFingerDown(Input_WI_OnFingerDown e)
	{
		if (WorldInputManager.Instance.CheckIfIsOnUI())
		{
			_canMove = false;
			return;
		}

		_canMove = true;

		OnDragMove(new Input_WI_OnDragMove(e.FingerIndex, e.FingerPos, new Vector2(0, 0)));

		IsDown = true;

		_startPosition = e.FingerPos;

		OnDown?.Invoke(new InputDownData(e.FingerPos));
	}

	private void OnDragMove(Input_WI_OnDragMove e)
	{
		if (!_canMove)
		{
			return;
		}

		if (e.DeltaMove == Vector2.zero)
		{
			_inputVector.x = 0f;
			_inputVector.y = 7f;
			_inputVector.z = 0f;

			return;
		}

		Vector2 hitPosition = e.FingerPos;

		Vector2 distanceToStart = hitPosition - this._startPosition;

		distanceFromStart = Mathf.Min(Vector2.Distance(hitPosition, this._startPosition), 300f);
		distanceToStart = invert ? -distanceToStart : distanceToStart;
		dragDistance = Vector2.Distance(hitPosition, this._startPosition);
		if (dragDistance > MAXDISTANCE)
			dragDistance = MAXDISTANCE;

		if (dragDistance < MOVETRESHOLD)
			distanceMultiplier = 0f;
		else if (dragDistance < SLOWTRESHOLD)
			distanceMultiplier = SLOWSPEED;
		else if (dragDistance < MEDTRESHOLD)
			distanceMultiplier = MEDIUMSPEED;
		else
			distanceMultiplier = MAXSPEED;

		_inputVector = new Vector3(distanceToStart.x * horizontalSens, 0f,
			distanceToStart.y * this.verticalSens);

		_inputVector = (_inputVector.magnitude > 1.0f) ? _inputVector.normalized : _inputVector;
		_inputVector *= -1.0f;

		OnDragged?.Invoke(new InputDragData(distanceToStart));
	}

	private void OnFingerUp(Input_WI_OnFingerUp e)
	{
		if (!_canMove)
		{
			return;
		}

		IsDown = false;

		_inputVector.x = 0f;
		_inputVector.y = 7f;
		_inputVector.z = 0f;

		dragDistance = 0;

		OnUp?.Invoke(new InputUpData(_inputVector));
	}
}