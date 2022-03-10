using MMFramework.TasksV2;
using ResetableSystem;
using System;
using UnityEngine;

public class Drop : MonoBehaviour,
    IResetable
{
    [SerializeField]
    private MMTaskExecutor _dropTaskExecutor =
        new MMTaskExecutor();

    [SerializeField]
    private MMTaskExecutor _droppedTaskExecutor =
        new MMTaskExecutor();

    public DropData DropData { get; set; }
    public Dropper Dropper { get; private set; }

    private DropClonerBase _dropCloner;
    private DropClonerBase _DropCloner
    {
        get
        {
            if (_dropCloner == null)
            {
                _dropCloner = GetComponent<DropClonerBase>();

                if (_dropCloner == null)
                    _dropCloner = gameObject.AddComponent<DropClonerInstantiate>();

            }

            return _dropCloner;
        }
    }

    public Action<Drop> OnDropping { get; set; }
    public Action<Drop> OnDropped { get; set; }

    public void Invoke(Dropper dropper, DropData dropData)
    {
        Dropper = dropper;
        DropData = dropData;
        
        OnDropping?.Invoke(this);

        gameObject.SetActive(true);

        _dropTaskExecutor.Execute(this, OnDropTasksExecuted);
    }

    private void OnDropTasksExecuted(bool didComplete)
    {
        _droppedTaskExecutor.Execute(this);

        OnDropped?.Invoke(this);
    }

    public Drop Clone()
    {
        return _DropCloner.CloneDrop(this);
    }

    public void ResetResetable()
    {
        _dropTaskExecutor.ResetResetable();
        _droppedTaskExecutor.ResetResetable();
    }
}