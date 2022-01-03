using System;
using UnityEngine;


public class GateHandler : MonoBehaviour
{
    [SerializeField] private BaseGateDetector _gateDetector;
    public Action<GateBase> OnGateCollided { get; set; }

    private void Awake()
    {
        _gateDetector.OnDetected += OnDetected;
    }

    private void OnDestroy()
    {
        _gateDetector.OnDetected -= OnDetected;
    }

    private void OnDetected(GateBase gate)
    {
        if (gate.TryCollide())
        {
            OnGateCollided?.Invoke(gate);
        }
    }
}