using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMUtils = MMFramework.Utilities.Utilities;

public abstract class ConsumerBase : MonoBehaviour, IAIInteractable
{
	[SerializeField] private EStorableType _consumeType;

	public EStorableType ConsumeType => _consumeType;

	[SerializeField] protected float _consumeDelayTime;
	[SerializeField] private StorableController _storableController;

	[SerializeField] private Collider _interactionArea;

	public Action<StorableBase> OnConsumed { get; set; }
	public Action OnConsumerStopped { get; set; }

	private void Start()
	{
		Consume();
	}

	private void Consume()
	{
		StartCoroutine(ConsumeRoutine());
	}

	private IEnumerator ConsumeRoutine()
	{
		while (true)
		{
			int storableCount = _storableController.StorableList.Count;

			if (storableCount == 0)
			{
				OnConsumerStopped?.Invoke();
				yield return null;
				continue;
			}

			var firstStorable = _storableController.StorableList[0];

			OnConsumed?.Invoke(firstStorable);

			yield return new WaitForSeconds(_consumeDelayTime);
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