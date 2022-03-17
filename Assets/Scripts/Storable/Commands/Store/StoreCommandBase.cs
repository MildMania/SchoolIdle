using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StoreCommandBase : ScriptableObject
{
    public Action OnStoreCommandFinished { get; set; }
    public List<StorableBase> StorableList { get; set; }
    public List<Transform>[] TargetTransforms { protected get; set; }
    public Transform ParentTransform { get; set; }
    
    
    protected StorableBase Storable;
    

    public void Execute(StorableBase storable)
    {
        Storable = storable;
        
        ExecuteCustomActions(storable, onStoreCommandExecuted);
        
        void onStoreCommandExecuted()
        {
            OnStoreCommandFinished?.Invoke();
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
