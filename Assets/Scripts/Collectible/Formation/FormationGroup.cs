using UnityEngine;


public enum EFormationGroupType
{
    Single = 0,
    Split = 1,
    Horizontal = 2,
    Producer = 3,
    Carrier = 4,
    Consumer = 5,
}

public class FormationGroup : MonoBehaviour
{
    public EFormationGroupType EFormationGroupType;

    public Transform[] LeadingTransforms;
}