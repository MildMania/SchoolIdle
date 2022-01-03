using UnityEngine;


public class UpgradeGate : GateBase
{
    protected override void OnEnterCustomActions()
    {
        Debug.Log("Upgrade Gate");
    }
}