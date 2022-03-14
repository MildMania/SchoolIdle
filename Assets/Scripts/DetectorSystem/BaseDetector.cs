using System;
using UnityEngine;

public abstract class BaseDetector<T> : MonoBehaviour
{
    public T LastDetected { get; set; }
    public Action<T> OnDetected;
    public Action<T> OnEnded;
    
}