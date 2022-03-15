using System;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyDropCommand", menuName = "ScriptableObjects/Storable/Drop/DestroyDropCommand",
    order = 1)]
public class DestroyDropCommand : DropCommandBase
{
    [SerializeField] private float _destroyDelayTime;
    
    protected override void ExecuteCustomActions(StorableBase storable, Action onStoreCommandExecuted)
    {
        storable.transform.SetParent(null);
        CoroutineRunner.Instance.StartCoroutine(DestroyItselfRoutine(storable));
        onStoreCommandExecuted?.Invoke();
    }

    private IEnumerator DestroyItselfRoutine(StorableBase storable)
    {
        yield return new WaitForSeconds(_destroyDelayTime);
        
        storable.DestroyItself();
        

    }
    
}