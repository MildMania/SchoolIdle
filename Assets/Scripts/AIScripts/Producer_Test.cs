using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.Utilities;

public class Producer_Test : MonoBehaviour
{
    [SerializeField] private float _productionRate;
    [SerializeField] private int _productionAmount;

    private int _currentResource = 0;
    public int CurrentResource => _currentResource;

    private void Awake()
    {
        _currentResource = _productionAmount;
    }

    public Vector3 GetInteractionPoint()
    {
        return transform.position;
    }

    public int TryReceive(int amount)
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
