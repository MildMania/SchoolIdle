using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StorableController : MonoBehaviour
{
    public List<StorableBase> StorableList { get; set; }
    
    [SerializeField] private Transform _storableContainer;

    public Transform StorableContainer => _storableContainer;

    private void Awake()
    {
        StorableList = new List<StorableBase>();
    }

}
