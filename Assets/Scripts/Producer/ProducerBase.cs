using System;
using System.Collections;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;


public abstract class ProducerBase : MonoBehaviour
{

	[SerializeField] protected int _produceLimit;
	[SerializeField] protected float _produceDelayTime;

	[SerializeField] private GameObject _toBeProducedPrefab;

	[SerializeField] private Transform _produceTarget;

	[SerializeField] private StorableDropHandler _storableDropHandler;
	[SerializeField] private StorableFormationController _storableFormationController;
	
	private int _numberOfProduced;
	
	public Action<StorableBase> OnProduced { get; set; }

	private void Awake()
	{
		_storableDropHandler.OnDropped += OnDropped;
	}

	private void OnDropped(StorableBase storable)
	{
		_numberOfProduced--;
		
		_storableFormationController.Reformat();
	}

	private void OnDestroy()
	{
		_storableDropHandler.OnDropped -= OnDropped;
	}

	private void Start()
	{
		Produce();
	}
	
	private void Produce()
	{
		StartCoroutine(ProduceRoutine());
	}

	private IEnumerator ProduceRoutine()
	{
		while (true)
		{
			if (_numberOfProduced >= _produceLimit)
			{
				yield return null;
				continue;
			}
				
			var producedObject = Instantiate(_toBeProducedPrefab);
			producedObject.transform.position = _produceTarget.position;
			
			
			var storable = producedObject.GetComponent<StorableBase>();

			_numberOfProduced++;
			
			OnProduced?.Invoke(storable);
			
			yield return new WaitForSeconds(_produceDelayTime);
		}
	}
	
	
}