using UnityEngine;


public class DegradeGate : GateBase
{
    protected override void OnEnterCustomActions()
    {
        Debug.Log("Degrade Gate");
    }
}