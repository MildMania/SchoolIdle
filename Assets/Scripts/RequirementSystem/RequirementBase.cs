using MMFramework.UserSystem;
using System;
using System.Collections.Generic;
using System.Linq;


public interface ISatisfyable
{
	bool TrySatisfy();
}

public interface IRequirement
{
	bool IsSatisfied();
	IRequirementStatus GetRequirementStatus();
	IConvertible RequirementType { get; }

	Action OnStatusUpdated { get; set; }

	void StartCheckingStatusUpdate();
	void FinishCheckingStatusUpdate();
}

public abstract class RequirementBase<T1> : IRequirement
	where T1 : IConvertible
{
	public T1 RequirementType
	{
		get { return GetRequirementType(); }
	}

	protected User User { get; private set; }

	IConvertible IRequirement.RequirementType
	{
		get { return RequirementType; }
	}

	#region Events

	public Action OnStatusUpdated { get; set; }

	#endregion

	public RequirementBase(
		User user)
	{
		User = user;
	}

	public abstract bool IsSatisfied();

	public abstract void StartCheckingStatusUpdate();

	public virtual void FinishCheckingStatusUpdate()
	{
	}

	public abstract IRequirementStatus GetRequirementStatus();

	protected abstract T1 GetRequirementType();

	public static void SortPriority(
		List<RequirementBase<T1>> reqList,
		List<T1> priorityList)
	{
		reqList = reqList.OrderBy(val =>
		{
			int index = priorityList.Count;

			if (priorityList.Contains(val.RequirementType))
				index = priorityList.IndexOf(val.RequirementType);

			return index;
		}).ToList();
	}
}

/// <summary>
/// 
/// </summary>
/// <typeparam name="T1">T defines the Requirement Type Enum</typeparam>
public abstract class RequirementBase<T1, TData> : RequirementBase<T1>
	where T1 : IConvertible
	where TData : struct, IRequirementData
{
	public readonly TData RequirementData;

	public RequirementBase(
		User user,
		TData data) : base(user)
	{
		RequirementData = data;
	}
}