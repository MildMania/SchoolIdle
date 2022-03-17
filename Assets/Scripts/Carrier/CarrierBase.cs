using UnityEngine;


public abstract class CarrierBase : MonoBehaviour
{
	[SerializeField] private int _carrierLimit;

	private int _numberOfCarried;

	private void Awake()
	{
		InitCarrierLimit();
		OnAwakeCustomActions();
	}

	protected virtual void OnAwakeCustomActions()
	{
	}

	private void InitCarrierLimit()
	{
		_numberOfCarried = 0;
	}

	public void IncreaseCarry()
	{
		_numberOfCarried++;
	}

	public void DecreaseCarry()
	{
		_numberOfCarried--;
		if (_numberOfCarried < 0)
		{
			_numberOfCarried = 0;
		}
	}

	public bool CanCarry()
	{
		return _numberOfCarried + 1 <= _carrierLimit;
	}
}