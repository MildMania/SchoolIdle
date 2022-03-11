using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseUncollectCommand : ScriptableObject
{
    public List<Collectible> CollectedCollectibles { get; set; }
    public Character Character { get; set; }
    public Action OnUncollectCommandStarted { get; }
    public Action OnUncollectCommandFinished { get; set; }
    private Collectible Collectible { get; set; }
    


    public void Execute(Collectible collectible)
    {
        Collectible = collectible;
        OnUncollectCommandStarted?.Invoke();
        if (collectible.MoveRoutine != null)
        {
            collectible.StopCommandExecution();
        }

        ExecuteCustomActions(collectible,
            () => OnUncollectCommandFinished?.Invoke());
    }

    public virtual void StopExecution()
    {
        if (Collectible != null && Collectible.MoveRoutine != null)
        {
            CoroutineRunner.Instance.StopCoroutine(Collectible.MoveRoutine);
        }
    }

    protected abstract void ExecuteCustomActions(
        Collectible collectible, Action onUncollectCommandExecuted);
}