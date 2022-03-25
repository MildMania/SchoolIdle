using System;
using System.Collections;
using UnityEngine;

public class Money : MonoBehaviour, IProducible, IConsumable
{
	private float _moveDuration = 0.5f;

	public Action<Money> OnMoveRoutineFinished { get; set; }
	private void OnDestroy()
	{
		StopAllCoroutines();
	}

	public void MoveProducible(Transform target, Transform container)
	{
		StartCoroutine(MoveRoutine(target, container, true));
	}


	public void MoveConsumable(Transform target, Transform container)
	{
		StartCoroutine(MoveRoutine(target, container, false));
	}

	private IEnumerator MoveRoutine(Transform target, Transform container, bool setActive)
	{
		float currentTime = 0;


		var producibleTransform = transform;
		Vector3 position = producibleTransform.position;


		while (currentTime < _moveDuration)
		{
			float step = currentTime / _moveDuration;

			producibleTransform.position = Vector3.Lerp(position,
				target.position, step);


			currentTime += Time.deltaTime;
			yield return null;
		}

		producibleTransform.position = target.position;
		producibleTransform.SetParent(container);
		producibleTransform.gameObject.SetActive(setActive);
		OnMoveRoutineFinished?.Invoke(this);
	}
}