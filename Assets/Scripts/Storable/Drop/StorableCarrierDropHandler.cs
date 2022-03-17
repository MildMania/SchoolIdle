using UnityEngine;

public class StorableCarrierDropHandler : StorableDropHandler
{
	[SerializeField] private CarrierBase _carrierBase;
	protected override void OnDropCustomActions()
	{
		base.OnDropCustomActions();
		_carrierBase.DecreaseCarry();
	}
}