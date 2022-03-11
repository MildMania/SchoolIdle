using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig",
    menuName = "GameConfig/Create a Game Config",
    order = 1)]
public class GameConfig : ScriptableObject
{
    [Header("CHARACTER")] 
    [SerializeField] private float _walkSpeed;

    [Header("STUDENT")] 
    [SerializeField] private float _doingAssignmentSpeed;


    [Header("PRINTER")]
    [SerializeField] private int _printerLimit;
    [Tooltip("Print Time Gap")]
    [SerializeField] private float _printerDelay;
    

    public float WalkSpeed => _walkSpeed;
    public float DoingAssignmentSpeed => _doingAssignmentSpeed;
    public int PrinterLimit => _printerLimit;
    public float PrinterDelay => _printerDelay;
    
}