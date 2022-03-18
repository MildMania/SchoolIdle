using System;


public interface IRequirementStatus
{
	IConvertible RequirementType { get; }
	bool IsSatisfied { get; }

	EStatus GetStatus(EStatus curStatus);

	Action OnStatusUpdated { get; set; }
}

public enum EStatus
{
	Invalid = 0,
	Upgradable = 1,
	NotSatisfied = 2,
	AtMaxLevel = 3
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T2">T2 defines the Requirement Type Enum</typeparam>
/// 
public abstract class RequirementStatusBase<T1, T2> : IRequirementStatus
	where T1 : IRequirement
	where T2 : IConvertible
{
	public T2 RequirementType => (T2) _requirement.RequirementType;

	IConvertible IRequirementStatus.RequirementType => RequirementType;

	public bool IsSatisfied => _requirement.IsSatisfied();

	private T1 _requirement;
	public T1 Requirement => _requirement;

	public Action OnStatusUpdated { get; set; }

	public RequirementStatusBase(T1 requirement)
	{
		_requirement = requirement;

		_requirement.OnStatusUpdated += OnReqStatusUpdated;
	}

	private void OnReqStatusUpdated()
	{
		OnStatusUpdated?.Invoke();
	}

	public abstract EStatus GetStatus(EStatus curStatus);
}