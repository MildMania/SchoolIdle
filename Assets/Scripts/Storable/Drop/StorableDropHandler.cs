using System;
using System.Collections;
using UnityEngine;

public abstract class StorableDropHandler : MonoBehaviour
{
    [SerializeField] protected StorableController _storableController;
    
    [SerializeField] private DropCommandBase _dropCommand;

    [SerializeField] private StorableFormationController _storableFormationController;

    private Coroutine _dropRoutine;
    
    public Action<StorableBase> OnStorableDropped { get; set; }

    
    private void Awake()
    {
        OnAwakeCustomActions();
    }

    protected virtual void OnAwakeCustomActions()
    {
        
    }
    private void OnDestroy()
    {
        OnOnDestroyCustomActions();
    }

    protected virtual void OnOnDestroyCustomActions()
    {
        
    }

    public void DropStorable(StorableBase storable)
    {
        storable.OnDropped += OnDropped;
        DropCommandBase dropCommandBase = CreateDropCommand();
        storable.Drop(dropCommandBase);
        OnStorableDropped?.Invoke(storable);
        OnDropCustomActions();
    }

    protected virtual void OnDropCustomActions()
    {
        
    }
    private void OnDropped(StorableBase storable)
    {
        _storableFormationController.Reformat();
        
        storable.OnDropped -= OnDropped;
    }
    
    public void StartDrop()
    {
        _dropRoutine = StartCoroutine(DropRoutine());
    }

    private IEnumerator DropRoutine()
    {
        var delay = new WaitForSeconds(0.2f);
        while (true)
        {
            int storableListCount = _storableController.StorableList.Count;

            if (storableListCount == 0)
            {
                yield return null;
                continue;
            }

            StorableBase droppedStorable;
            if (_storableController.LastDropable != null)
            {
                droppedStorable = _storableController.LastDropable;
            }
            else
            {
                droppedStorable = _storableController.StorableList[storableListCount - 1];
            }
            
            _storableController.StorableList.Remove(droppedStorable);
            DropStorable(droppedStorable);

            yield return delay;
        }
    }

    public void StopDrop()
    {
        if (_dropRoutine == null)
        {
            return;
        }
         
        StopCoroutine(_dropRoutine);
    }
    
    private DropCommandBase CreateDropCommand()
    {
        DropCommandBase dropCommand = Instantiate(_dropCommand);
        dropCommand.StorableList = _storableController.StorableList;

        return dropCommand;
    }
}
