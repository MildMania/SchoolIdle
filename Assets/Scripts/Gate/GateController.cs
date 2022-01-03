using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateController : MonoBehaviour
{
    private GateBase[] _gates;

    private void Awake()
    {
        _gates = GetComponentsInChildren<GateBase>();

        foreach (var gate in _gates)
        {
            gate.OnEntered += OnEntered;
        }
    }

    private void OnDestroy()
    {
        foreach (var gate in _gates)
        {
            gate.OnEntered -= OnEntered;
        }
    }

    private void OnEntered(GateBase gate)
    {
        foreach (var item in _gates)
        {
            if (item != gate)
            {
                item.Deactivate();
            }
        }
    }
}