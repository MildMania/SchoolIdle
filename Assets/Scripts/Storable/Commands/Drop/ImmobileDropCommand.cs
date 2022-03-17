using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ImmobileDropCommand", menuName = "ScriptableObjects/Storable/Drop/ImmobileDropCommand",
    order = 1)]
public class ImmobileDropCommand : DropCommandBase
{
    protected override void ExecuteCustomActions(StorableBase storable, Action onStoreCommandExecuted)
    {

        onStoreCommandExecuted?.Invoke();
    }
}
