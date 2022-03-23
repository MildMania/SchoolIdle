using System;
using System.Collections;
using UnityEngine;

using MMUtils = MMFramework.Utilities.Utilities;

public abstract class ProducerBase : MonoBehaviour, IUnlockable, IAIInteractable
{
	[SerializeField] public Unlockable Unlockable { get; }
	
	[SerializeField] protected int _produceLimit;

	[SerializeField] private int _limitPerProduce;

	[SerializeField] protected float _produceDelayTime;

	[SerializeField] private GameObject _toBeProducedPrefab;

	[SerializeField] private Transform _produceTarget;

	[SerializeField] private StorableDropHandler _storableDropHandler;

	//-----------
	[SerializeField] private Collider _interactionArea;
	//-----------

	protected Coroutine _produceRoutine;

	private int _numberOfProduced;
	public EStorableType ProducedStorableType { get; set; }
	public Action<StorableBase> OnProduced { get; set; }

	private void Awake()
	{
		ProducedStorableType = _toBeProducedPrefab.GetComponentInChildren<StorableBase>().StorableType;
		_storableDropHandler.OnStorableDropped += OnDropped;
		OnAwakeCustomActions();
	}

	private void Start()
	{
		OnStartCustomActions();
	}

	protected virtual void OnStartCustomActions()
	{
	}

	protected virtual void OnAwakeCustomActions()
	{
	}

	protected virtual void OnDroppedCustomActions()
	{
	}

	private void OnDropped(StorableBase storable)
	{
		_numberOfProduced--;
		OnDroppedCustomActions();
	}

	private void OnDestroy()
	{
		_storableDropHandler.OnStorableDropped -= OnDropped;
		OnDestroyCustomActions();
	}

	protected virtual void OnDestroyCustomActions()
	{
	}

	public void StartProduce()
	{
		_produceRoutine = StartCoroutine(ProduceRoutine());
	}

	public void StopProduce()
	{
		if (_produceRoutine != null)
		{
			StopCoroutine(_produceRoutine);
			_produceRoutine = null;
		}
	}

	private IEnumerator ProduceRoutine()
	{
		var delay = new WaitForSeconds(_produceDelayTime);
		while (true)
		{
			yield return delay;

			if (_numberOfProduced >= _produceLimit)
			{
				yield return null;
				continue;
			}

			for (int i = 0; i < _limitPerProduce; i++)
			{
				var producedObject = Instantiate(_toBeProducedPrefab);
				producedObject.transform.position = _produceTarget.position;

				var storable = producedObject.GetComponent<StorableBase>();
				_numberOfProduced++;
				OnProduced?.Invoke(storable);

				yield return null;
			}
		}
	}

	//TODO : SOLVE MERGE CONFLICTS

	public Vector3 GetInteractionPoint()
	{
		Vector3 center = _interactionArea.transform.position;
		Vector3 randomInBound = MMUtils.RandomPointInBounds(_interactionArea.bounds);

		randomInBound = new Vector3(randomInBound.x, 0, randomInBound.z);

		Vector3 interactionPoint = center + randomInBound;

		return interactionPoint;
	}

}