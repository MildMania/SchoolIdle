using System.Collections;
using UnityEngine;

public class StorableCarrierStoreHandler : StorableStoreHandler
{
	[SerializeField] private BaseProducerDetector _baseProducerDetector;

	[SerializeField] private MovementIdleState _movementIdleState;

	private Coroutine _checkIdleStateRoutine;

	private bool _onIdleState;

	private bool _isDropRoutineRun;

	private void Awake()
	{
		_movementIdleState.OnIdleStateEnter += OnIdleStateEnter;
		_movementIdleState.OnIdleStateExit += OnIdleStateExit;
		_baseProducerDetector.OnDetected += OnProducerDetected;
		_baseProducerDetector.OnEnded += OnProducerEnded;
	}

	private void OnIdleStateExit()
	{
		_onIdleState = false;
	}

	private void OnIdleStateEnter()
	{
		_onIdleState = true;
	}


	private void OnDestroy()
	{
		_baseProducerDetector.OnDetected -= OnProducerDetected;
		_baseProducerDetector.OnEnded -= OnProducerEnded;
	}

	private void OnProducerDetected(ProducerBase producerBase)
	{
		_checkIdleStateRoutine = StartCoroutine(CheckIdleStateRoutine(producerBase));
	}

	private void OnStorableDropped(StorableBase storableBase)
	{
		_storableFormationController.Reformat();
		StoreStorable(storableBase);
	}

	private IEnumerator CheckIdleStateRoutine(ProducerBase producerBase)
	{
		var storableDropHandler = producerBase.GetComponentInChildren<StorableDropHandler>();
		storableDropHandler.OnStorableDropped += OnStorableDropped;
		while (true)
		{
			if (_onIdleState && !_isDropRoutineRun)
			{
				storableDropHandler.StartDrop();
				_isDropRoutineRun = true;
				yield return null;
				continue;
			}

			if (!_onIdleState)
			{
				storableDropHandler.StopDrop();
				_isDropRoutineRun = false;
			}

			yield return null;
		}
	}

	private void OnProducerEnded(ProducerBase producerBase)
	{
		if (_checkIdleStateRoutine != null)
		{
			StopCoroutine(_checkIdleStateRoutine);
			_checkIdleStateRoutine = null;
			var storableDropHandler = producerBase.GetComponentInChildren<StorableDropHandler>();
			storableDropHandler.StopDrop();
			storableDropHandler.OnStorableDropped -= OnStorableDropped;
		}
	}
}