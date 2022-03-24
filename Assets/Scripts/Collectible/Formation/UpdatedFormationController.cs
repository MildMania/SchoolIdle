using System.Collections.Generic;
using UnityEngine;

public class UpdatedFormationController : MonoBehaviour
{
    [SerializeField] private Transform _container;

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

    public Transform Container
    {
        get => _container;
        set => _container = value;
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


    public Transform GetLastTargetTransform(Transform producibleTransform)
    {
        Transform clonedTransform = Instantiate(producibleTransform, transform);
        clonedTransform.gameObject.SetActive(false);
        CurrentRow = _addedTransformCount / TargetTransforms.Length;
        CurrentColumn = _addedTransformCount % TargetTransforms.Length;
        clonedTransform.position = TargetTransforms[CurrentColumn][CurrentRow].position + _distance;
        TargetTransforms[CurrentColumn].Add(clonedTransform);
        _addedTransformCount++;
        return clonedTransform;
    }

    public Transform GetLastTransform()
    {
        if (_addedTransformCount == 0)
        {
            return null;
        }

        CurrentRow = _addedTransformCount / TargetTransforms.Length;
        CurrentColumn = _addedTransformCount % TargetTransforms.Length;
        Transform targetTransform = TargetTransforms[CurrentColumn][CurrentRow];
        TargetTransforms[CurrentColumn].Remove(targetTransform);
        _addedTransformCount--;
        return targetTransform;
    }
}