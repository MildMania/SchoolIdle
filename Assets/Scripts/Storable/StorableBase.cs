using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StorableBase : MonoBehaviour
{
    public Action<StorableBase> OnStored { get; set; }
    public Action<StorableBase> OnDropped { get; set; }
    
    private StoreCommandBase _storeCommand;
    private DropCommandBase _dropCommand;

    public IEnumerator MoveRoutine;
    
    
    
    public void Store(StoreCommandBase storeCommand)
    {
        if (storeCommand == null)
        {
            return;
        }

        _storeCommand = storeCommand;
        storeCommand.Execute(this);
        storeCommand.OnStoreCommandFinished += OnStoreCommandFinished;
    }
    
    private void OnStoreCommandFinished()
    {
        if (_storeCommand == null)
        {
            return;
        }
        
        OnStored?.Invoke(this);
        _storeCommand.OnStoreCommandFinished -= OnStoreCommandFinished;
    }
    
    

    public void Drop(DropCommandBase dropCommand)
    {
        if (_dropCommand == null)
        {
            return;
        }

        _dropCommand = dropCommand;
        dropCommand.Execute(this);
        dropCommand.OnDropCommandFinished += OnDropCommandFinished;
    }
    
    private void OnDropCommandFinished()
    {
        if (_dropCommand == null)
        {
            return;
        }
        
        OnDropped?.Invoke(this);
        _dropCommand.OnDropCommandFinished -= OnDropCommandFinished;
    }
    
    public void StopCommandExecution()
    {
        if (_storeCommand != null)
        {
            _storeCommand.StopExecute();
        }

        if (_dropCommand != null)
        {
            _dropCommand.StopExecute();
        }
    }
}