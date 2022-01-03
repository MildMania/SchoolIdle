using UnityEngine;


public class UpgradeGate : GateBase
{
	public override void OnEnterCustomActions()
	{
		Debug.Log("Upgrade Gate");
	}
}