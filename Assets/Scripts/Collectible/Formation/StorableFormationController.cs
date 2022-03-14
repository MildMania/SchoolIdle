using System.Collections.Generic;
using UnityEngine;

public class StorableFormationController : MonoBehaviour
{
    [SerializeField] private StorableController _storableController;
    [SerializeField] private StorableStoreHandler _storableStoreHandler;

    private readonly Dictionary<EFormationGroupType, Transform[]> _formationGroupTypeToLeadingTransforms =
        new Dictionary<EFormationGroupType, Transform[]>();

    private List<Transform>[] _targetTransforms;

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


    private void Awake()
    {
        foreach (var formationGroup in GetComponentsInChildren<FormationGroup>())
        {
            _formationGroupTypeToLeadingTransforms[formationGroup.EFormationGroupType] =
                formationGroup.LeadingTransforms;
        }

        InitTargetTransforms(CurrentFormationGroupType);
    }


    public void Reformat()
    {
        ChangeFormationGroupType(CurrentFormationGroupType);
    }

    private void InitTargetTransforms(EFormationGroupType eFormationGroupType)
    {
        TargetTransforms =
            new List<Transform>[_formationGroupTypeToLeadingTransforms[eFormationGroupType].Length];
        for (int i = 0; i < TargetTransforms.Length; i++)
        {
            TargetTransforms[i] = new List<Transform>
                { _formationGroupTypeToLeadingTransforms[eFormationGroupType][i] };
        }
    }

    public void ChangeFormationGroupType(EFormationGroupType eFormationGroupType)
    {
        CurrentFormationGroupType = eFormationGroupType;

        InitTargetTransforms(CurrentFormationGroupType);

        List<StorableBase> storableList = _storableController.StorableList;
        foreach (var storable in storableList)
        {
            storable.StopCommandExecution();
        }

        _storableController.StorableList = new List<StorableBase>();

                

        foreach (var storable in storableList)
        {
            StoreCommandBase storeCommandBase = _storableStoreHandler.CreateStoreCommand();
            storable.Store(storeCommandBase);
        }

    }
}