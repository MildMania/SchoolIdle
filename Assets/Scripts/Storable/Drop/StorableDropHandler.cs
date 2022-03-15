using System;
using System.Collections;
using UnityEngine;

public abstract class StorableDropHandler : MonoBehaviour
{
    
    [SerializeField] private StorableController _storableController;
    
    [SerializeField] private DropCommandBase _dropCommand;

    [SerializeField] private StorableFormationController _storableFormationController;
    
    private DropCommandBase _dropCommandClone;
    
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

    public void Drop(StorableBase storable)
    {
        storable.OnDropped += OnDropped;

        DropCommandBase dropCommandBase = CreateDropCommand();
        storable.Drop(dropCommandBase);
    }

    private void OnDropped(StorableBase storable)
    {
        Debug.Log(storable.gameObject.name + " OBJECT DROPPED!");
        _storableFormationController.Reformat();
        storable.OnDropped -= OnDropped;
    }
    
    public void StartDrop()
    {
        _dropRoutine = StartCoroutine(DropRoutine());
    }

    private IEnumerator DropRoutine()
    {
        while (true)
        {
            int storableListCount = _storableController.StorableList.Count;

            if (storableListCount == 0)
            {
                yield return null;
                continue;
            }
            
            StorableBase droppedStorable = _storableController.StorableList[storableListCount - 1];
            _storableController.StorableList.Remove(droppedStorable);
            Drop(droppedStorable);
            OnStorableDropped?.Invoke(droppedStorable);
            
            yield return new WaitForSeconds(0.2f);
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
