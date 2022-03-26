using UnityEngine;

public abstract class BaseProductionRequirement : MonoBehaviour
{
    public abstract bool IsProductionRequirementMet();

    public abstract void ConsumeRequirements();
}