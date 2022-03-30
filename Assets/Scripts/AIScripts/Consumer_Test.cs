using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.Utilities;

public class Consumer_Test : MonoBehaviour
{
    [SerializeField] private float _consumeRate;
    [SerializeField] private int _consumeAmount;

    [SerializeField] private int _maxCapacity;
    public int Capacity
    {
        get => _maxCapacity - _currentResource;
    }

    private int _currentResource;
    public int CurrentResource => _currentResource;


    public Vector3 GetInteractionPoint()
    {
        return transform.position;
    }

    public int TryDeliver(int amount)
    {
        if (_currentResource - amount >= 0)
        {
            _currentResource -= amount;
            return amount;
        }
        else
            return 0;
    }
}
