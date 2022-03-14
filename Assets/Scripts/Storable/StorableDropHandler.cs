using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableDropHandler : MonoBehaviour
{
    
    [SerializeField] private StorableController _storableController;
    
    [SerializeField] private DropCommandBase _dropCommand;
    private DropCommandBase _dropCommandClone;
    
    private Coroutine _dropRoutine;
    
    public Action<StorableBase> OnDropped { get; set; }

    private bool _canDrop;
    
    private void Awake()
    {
       
    }

    private void OnDestroy()
    {
       
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
            
            OnDropped?.Invoke(droppedStorable);
            
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
}
