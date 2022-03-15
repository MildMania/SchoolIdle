using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropCommandBase : ScriptableObject
{
	public Action OnDropCommandFinished { get; set; }
	public List<StorableBase> StorableList { get; set; }
	
	protected StorableBase Storable;


	public void Execute(StorableBase storable)
	{
		Storable = storable;

		ExecuteCustomActions(storable, onDropCommandExecuted);

		void onDropCommandExecuted()
		{
			OnDropCommandFinished?.Invoke();
		}
	}

	public void StopExecute()
	{
		if (Storable != null && Storable.MoveRoutine != null)
		{
			CoroutineRunner.Instance.StopCoroutine(Storable.MoveRoutine);
		}
	}

	protected abstract void ExecuteCustomActions(
		StorableBase storable, Action onStoreCommandExecuted);
}