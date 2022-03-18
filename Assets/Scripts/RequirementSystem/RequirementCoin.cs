using UnityEngine;
using WarHeroes.InventorySystem;


public class RequirementStatus_Resource : RequirementStatusBase<RequirementCoin, ERequirement>
{
	public RequirementStatus_Resource(RequirementCoin requirement)
		: base(requirement)
	{
	}

	public override EStatus GetStatus(EStatus curStatus)
	{
		if (!IsSatisfied)
			return EStatus.NotSatisfied;

		return curStatus;
	}
}

public struct RequirementDataCoin : IRequirementData
{
	[SerializeField] private ECoin _coinType;
	public ECoin CoinType => _coinType;

	[SerializeField] private int _requiredAmount;
	public int RequiredAmount => _requiredAmount;

	public RequirementDataCoin(
		ECoin coinType,
		int amount)
	{
		_coinType = coinType;
		_requiredAmount = amount;
	}

	public IRequirement CreateRequirement(User user)
	{
		return new RequirementCoin(user, this);
	}
}

public class RequirementCoin : RequirementBase<ERequirement, RequirementDataCoin>,
	ISatisfyable,IFillable
{
	public RequirementCoin(User user, RequirementDataCoin data)
		: base(user, data)
	{
	}

	public override IRequirementStatus GetRequirementStatus()
	{
		return new RequirementStatus_Resource(this);
	}

	public override bool IsSatisfied()
	{
		IInventory<EInventory> inventory;
		UserManager.Instance.LocalUser.InventoryController.TryGetInventoryOfType(EInventory.Coin,out inventory);

		CoinInventory coinInventory = (CoinInventory) inventory;
		
		return coinInventory.CanAfford(
			RequirementData.CoinType,
			RequirementData.RequiredAmount);
	}

	public override void StartCheckingStatusUpdate()
	{
	}

	protected override ERequirement GetRequirementType()
	{
		return ERequirement.Coin;
	}

	public bool TrySatisfy()
	{
		IInventory<EInventory> inventory;
		UserManager.Instance.LocalUser.InventoryController.TryGetInventoryOfType(EInventory.Coin,out inventory);
		
		CoinInventory coinInventory = (CoinInventory) inventory;

		return coinInventory.TryIncreaseCount(
			RequirementData.CoinType,
			-RequirementData.RequiredAmount);
	}

	public Fillable Fillable { get; }
}