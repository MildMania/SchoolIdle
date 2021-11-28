using System.Collections.Generic;
using UnityEngine;

public class FormationController : MonoBehaviour
{
    [SerializeField] private CollectibleController _collectibleController;
    [SerializeField] private CollectibleCollector _collectibleCollector;

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

        List<Collectible> collectibles = _collectibleController.CollectedCollectibles;
        foreach (var c in collectibles)
        {
            c.StopCommandExecution();
            c.transform.parent = null;
        }

        _collectibleController.CollectedCollectibles = new List<Collectible>();


        foreach (var c in collectibles)
        {
            if (!_collectibleController.CollidedCollectibles.Contains(c))
            {
                BaseCollectCommand collectCommandClone = _collectibleCollector.CreateCommand();
                c.IsCollected = false;
                c.TryCollect(collectCommandClone);
            }
        }

        _collectibleController.CollidedCollectibles = new List<Collectible>();
    }
}