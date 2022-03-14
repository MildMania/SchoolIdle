using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DropCommandBase : ScriptableObject
{
    public Action OnDropCommandFinished { get; set; }

    public void Execute(StorableBase storable)
    {
        
    }

    public void StopExecute()
    {
        
    }
}
