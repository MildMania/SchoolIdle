using System;
using System.Collections;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;


public abstract class Producer : MonoBehaviour, IProducible
{
	public BaseSpawner _baseSpawner;

	[SerializeField] protected int _produceLimit;

	[SerializeField] protected float _produceDelay;

	private int _numberOfProduced;

	[PhaseListener(typeof(GamePhase), true)]
	public void StartProduceRoutine()
	{
		StartCoroutine(ProduceRoutine());
	}

	private void Awake()
	{
		AwakeCustomActions();	
	}
	
	protected virtual void  AwakeCustomActions()
	{
		
	}
	private IEnumerator ProduceRoutine()
	{
		while (true)
		{
			if (_numberOfProduced < _produceLimit)
			{
				Produce();
				yield return new WaitForSeconds(_produceDelay);
			}

			yield return null;
		}
	}

	public void Produce()
	{
		_baseSpawner.SpawnCollectible();
		_numberOfProduced++;
	}
}