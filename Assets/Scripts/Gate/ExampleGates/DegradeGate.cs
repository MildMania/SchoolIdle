using UnityEngine;


public class DegradeGate : GateBase
{
	public override void OnEnterCustomActions()
	{
		Debug.Log("Degrade Gate");
	}
}