using System;
using MMFramework.TasksV2;
using UnityEngine;


public abstract class GateBase : MonoBehaviour
{
    [SerializeField] protected MMTaskExecutor _onCollidedTasks;

    public bool IsCollided { get; set; }

    protected abstract void OnEnterCustomActions();

    public Action<GateBase> OnEntered;

    public virtual void Deactivate()
    {
        IsCollided = true;
    }

    public bool TryCollide()
    {
        if (IsCollided)
        {
            return false;
        }

        OnEnterCustomActions();
        Deactivate();
        _onCollidedTasks?.Execute(this);
        OnEntered?.Invoke(this);
        return true;
    }
}