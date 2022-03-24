using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carrier_Test : MonoBehaviour
{
    [SerializeField] private int _maxCapacity;

    private int _currentResource = 0;

    public int Capacity
    {
        get => _maxCapacity - _currentResource;
    }

    public void Store(Producer_Test producer)
    {
        if(_currentResource + producer.CurrentResource <= _maxCapacity)
        {
            _currentResource += producer.CurrentResource;
        }
        else
        {
            _currentResource = _maxCapacity;
        }
    }

    public void Deliver(Consumer_Test consumer)
    {
        int delivered = consumer.TryDeliver(_currentResource);
        _currentResource -= delivered;
    }



}
