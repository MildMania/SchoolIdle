using System;
using System.Collections;
using UnityEngine;


public abstract class ProducerBase : MonoBehaviour
{
	[SerializeField] protected int _produceLimit;

	[SerializeField] private int _limitPerProduce;
	
	[SerializeField] protected float _produceDelayTime;

	[SerializeField] private GameObject _toBeProducedPrefab;

	[SerializeField] private Transform _produceTarget;

	[SerializeField] private StorableDropHandler _storableDropHandler;

	protected Coroutine _produceRoutine;
	private int _numberOfProduced;
	
	public Action<StorableBase> OnProduced { get; set; }

	private void Awake()
	{
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
		while (true)
		{
			yield return new WaitForSeconds(_produceDelayTime);
			
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
}