using System;
using System.Collections.Generic;
using System.Linq;
using MMFramework.StatSystem;

public static class RequirementUtilities
{
	public static IRequirement[] GetRequirements(
		User user,
		IRequirementData[] datum)
	{
		IRequirement[] reqs = new IRequirement[datum.Length];

		for (int i = 0; i < datum.Length; i++)
			reqs[i] = datum[i].CreateRequirement(user);

		return reqs;
	}

	public static IRequirementStatus[] GetRequirementStatuses<T>(
		IRequirement[] reqs,
		List<T> priorityList = null)
		where T : IConvertible
	{
		IRequirementStatus[] statuses = new IRequirementStatus[reqs.Length];

		for (int i = 0; i < reqs.Length; i++)
			statuses[i] = reqs[i].GetRequirementStatus();

		if (priorityList == null)
			return statuses;

		RequirementBase<T>.SortPriority(
			reqs.Cast<RequirementBase<T>>().ToList(),
			priorityList);

		return statuses;
	}

	public static bool AreRequirementsSatisfied(
		User user,
		IRequirementData[] reqDatum)
	{
		foreach (IRequirementData data in reqDatum)
		{
			IRequirement requirement = data.CreateRequirement(user);

			if (!requirement.IsSatisfied())
				return false;
		}

		return true;
	}

	public static bool TrySatisfyRequirements(
		User user,
		IRequirementData[] reqDatum)
	{

		foreach (IRequirementData data in reqDatum)
		{
			IRequirement requirement = data.CreateRequirement(user);
			
			if (requirement is FillableCoinRequirement fillableCoinRequirement)
			{
				fillableCoinRequirement.TryFill(user);
			}
		}

		if (!AreRequirementsSatisfied(user, reqDatum))
		{
			return false;
		}
		
		foreach (IRequirementData data in reqDatum)
		{
			IRequirement requirement = data.CreateRequirement(user);

			if (!(requirement is ISatisfyable satisfyableReq))
				continue;

			if (!satisfyableReq.TrySatisfy())
				return false;
		}

		return true;
	}
}