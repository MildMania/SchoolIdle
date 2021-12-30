using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleCollector : MonoBehaviour
{
    [SerializeField] private BaseCollectCommand _collectCommand;
    [SerializeField] private Transform _collectibleContainer;
    [SerializeField] private CollectibleController _collectibleController;
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private FormationController _formationController;

    private BaseCollectibleDetector[] _collectibleDetectors;

    public Action<Collectible> OnCollectibleCollected { get; set; }

    public BaseCollectibleDetector[] CollectibleDetectors
    {
        get
        {
            if (_collectibleDetectors == null)
            {
                _collectibleDetectors = GetComponentsInChildren<BaseCollectibleDetector>();
            }

            return _collectibleDetectors;
        }
    }

    public BaseCollectCommand CollectCommand
    {
        get => _collectCommand;
        set => _collectCommand = value;
    }


    private bool _isTransformed = false;

    private void Awake()
    {
        foreach (var collectibleDetector in CollectibleDetectors)
        {
            collectibleDetector.OnDetected += OnDetected;
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var collectibleDetector in CollectibleDetectors)
        {
            collectibleDetector.OnDetected -= OnDetected;
        }

        foreach (var collectedCollectible in _collectibleController.CollectedCollectibles)
        {
            collectedCollectible.StopCommandExecution();
        }
    }

    public void OnDetected(Collectible collectible)
    {
        BaseCollectCommand collectCommandClone = null;
        if (_collectCommand != null)
        {
            collectCommandClone = CreateCommand();
        }

        collectible.OnCollected += OnCollected;
        collectible.TryCollect(collectCommandClone);
    }

    private void OnCollected(Collectible collectible)
    {
        collectible.OnCollected -= OnCollected;
        OnCollectibleCollected?.Invoke(collectible);
    }

    public BaseCollectCommand CreateCommand()
    {
        BaseCollectCommand collectCommandClone = Instantiate(CollectCommand);
        collectCommandClone.CharacterTransform = _characterTransform;
        collectCommandClone.ParentTransform = _collectibleContainer;
        collectCommandClone.CollectedCollectibles = _collectibleController.CollectedCollectibles;
        collectCommandClone.TargetTransforms = _formationController.TargetTransforms;

        return collectCommandClone;
    }
}