using System.Collections.Generic;
using UnityEngine;

public class UpdatedFormationController : MonoBehaviour
{
    private readonly Dictionary<EFormationGroupType, Transform[]> _formationGroupTypeToLeadingTransforms =
        new Dictionary<EFormationGroupType, Transform[]>();

    private List<Transform>[] _targetTransforms;

    public Vector3 _distance;


    private int _addedTransformCount;

    public List<Transform>[] TargetTransforms
    {
        get => _targetTransforms;
        set => _targetTransforms = value;
    }

    [SerializeField] private EFormationGroupType _currentFormationGroupType;


    public EFormationGroupType CurrentFormationGroupType
    {
        get => _currentFormationGroupType;
        set => _currentFormationGroupType = value;
    }

    public int CurrentColumn { get; set; }

    public int CurrentRow { get; set; }

    public Vector3 Distance
    {
        get => _distance;
        set => _distance = value;
    }


    private void Awake()
    {
        foreach (var formationGroup in GetComponentsInChildren<FormationGroup>())
        {
            _formationGroupTypeToLeadingTransforms[formationGroup.EFormationGroupType] =
                formationGroup.LeadingTransforms;
        }

        InitTargetTransforms(CurrentFormationGroupType);
    }

    private void InitTargetTransforms(EFormationGroupType eFormationGroupType)
    {
        TargetTransforms =
            new List<Transform>[_formationGroupTypeToLeadingTransforms[eFormationGroupType].Length];
        for (int i = 0; i < TargetTransforms.Length; i++)
        {
            TargetTransforms[i] = new List<Transform>
                {_formationGroupTypeToLeadingTransforms[eFormationGroupType][i]};
        }
    }


    public Transform GetLastTargetTransform(List<IProducible> objects, Transform producibleTransform)
    {
        Transform clonedTransform = Instantiate(producibleTransform);
        CurrentRow = _addedTransformCount / TargetTransforms.Length;
        CurrentColumn = _addedTransformCount % TargetTransforms.Length;
        clonedTransform.position = TargetTransforms[CurrentColumn][CurrentRow].position + _distance;
        TargetTransforms[CurrentColumn].Add(clonedTransform);
        _addedTransformCount++;
        return clonedTransform;
    }
}