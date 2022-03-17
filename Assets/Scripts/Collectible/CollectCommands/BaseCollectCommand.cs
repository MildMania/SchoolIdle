using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseCollectCommand : ScriptableObject
{
	public Action OnCollectCommandStarted { get; }
	public Action OnCollectCommandFinished { get; set; }

	public Transform CharacterTransform { protected get; set; }
	public List<Transform>[] TargetTransforms { protected get; set; }
	public Transform ParentTransform { protected get; set; }
	private Collectible Collectible { get; set; }


	public List<Collectible> CollectedCollectibles { get; set; }


	public void Execute(Collectible collectible)
	{
		Collectible = collectible;
		// Collectible.transform.parent = ParentTransform;
		OnCollectCommandStarted?.Invoke();
		ExecuteCustomActions(collectible, onCollectCommandExecuted);


		void onCollectCommandExecuted()
		{
			OnCollectCommandFinished?.Invoke();
		}
	}

	public virtual void StopExecution()
	{
		if (Collectible != null && Collectible.MoveRoutine != null)
		{
			CoroutineRunner.Instance.StopCoroutine(Collectible.MoveRoutine);
		}
	}


	protected abstract void ExecuteCustomActions(
		Collectible collectible, Action onCollectCommandExecuted);
}