using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableConsumerStoreHandler : StorableStoreHandler
{
	[SerializeField] private BaseCharacterDetector _baseCharacterDetector;

	[SerializeField] private ConsumerBase _consumerBase;

	private Coroutine _checkDropRoutine;

	private bool _isDropRoutineRun;

	private bool _onIdleState;

	private void Awake()
	{
		_baseCharacterDetector.OnDetected += OnCharacterDetected;
		_baseCharacterDetector.OnEnded += OnCharacterEnded;
	}

	private void OnDestroy()
	{
		_baseCharacterDetector.OnDetected -= OnCharacterDetected;
		_baseCharacterDetector.OnEnded -= OnCharacterEnded;
	}

	private void OnCharacterDetected(Character character)
	{
		var storableDropHandler = character.GetComponentInChildren<StorableDropHandler>();
		var characterIdleState = character.GetComponentInChildren<MovementIdleState>();
		storableDropHandler.OnStorableDropped += OnStorableDropped;
		characterIdleState.OnIdleStateEnter += OnIdleStateEnter;
		characterIdleState.OnIdleStateExit += OnIdleStateExit;

		var storableController = character.GetComponentInChildren<StorableController>();

		_checkDropRoutine = StartCoroutine(CheckDropRoutine(storableController, storableDropHandler));
	}

	private void OnIdleStateExit()
	{
		_onIdleState = false;
	}

	private void OnIdleStateEnter()
	{
		_onIdleState = true;
	}

	private IEnumerator CheckDropRoutine(StorableController characterStorableController, StorableDropHandler
											characterDropHandler)
	{
		while (true)
		{
			if (_onIdleState && characterStorableController.TryDropForConsume(_consumerBase.ConsumeType))
			{
				if (!_isDropRoutineRun)
				{
					characterDropHandler.StartDrop();
					_isDropRoutineRun = true;
				}

				yield return null;
				continue;
			}

			characterDropHandler.StopDrop();
			_isDropRoutineRun = false;

			yield return null;
		}
	}

	private void OnStorableDropped(StorableBase storable)
	{
		StoreStorable(storable);
	}

	private void OnCharacterEnded(Character character)
	{
		var storableDropHandler = character.GetComponentInChildren<StorableDropHandler>();
		var characterIdleState = character.GetComponentInChildren<MovementIdleState>();
		storableDropHandler.OnStorableDropped -= OnStorableDropped;
		characterIdleState.OnIdleStateEnter -= OnIdleStateEnter;
		characterIdleState.OnIdleStateExit -= OnIdleStateExit;

		if (!_isDropRoutineRun)
		{
			storableDropHandler.StopDrop();
		}

		if (_checkDropRoutine != null)
		{
			StopCoroutine(_checkDropRoutine);
			_checkDropRoutine = null;
		}
	}
}