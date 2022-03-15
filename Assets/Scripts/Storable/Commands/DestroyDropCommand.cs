using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DestroyDropCommand", menuName = "ScriptableObjects/Storable/Drop/DestroyDropCommand",
    order = 1)]
public class DestroyDropCommand : DropCommandBase
{
    protected override void ExecuteCustomActions(StorableBase storable, Action onStoreCommandExecuted)
    {
        Debug.Log("Storable game object name: " + storable);
        //Destroy(storable.gameObject);
        onStoreCommandExecuted?.Invoke();
    }
}