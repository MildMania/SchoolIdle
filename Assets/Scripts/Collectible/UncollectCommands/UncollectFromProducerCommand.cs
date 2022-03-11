using System;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "UncollectFromProducerCommand",
    menuName = "ScriptableObjects/Uncollect/UncollectFromProducerCommand",
    order = 1)]
public class UncollectFromProducerCommand : BaseUncollectCommand
{
    protected override void ExecuteCustomActions(Collectible collectible,
        Action onUncollectCommandExecuted)
    {
     
        collectible.transform.SetParent(null);
        var collectibleGO = collectible.gameObject;
        
        var collectibleCollider = collectibleGO.GetComponent<Collider>();
        
        collectibleCollider.enabled = true;
        collectibleCollider.isTrigger = false;
        
        //collectible.transform.position = Character.transform.position;
        collectible.transform.position = Vector3.zero;
        //collectible.IsCollected = false;

        
        onUncollectCommandExecuted?.Invoke();
    }
}