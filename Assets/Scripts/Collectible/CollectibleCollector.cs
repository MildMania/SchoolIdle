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

    [SerializeField] private BaseCollectibleDetector _collectibleDetector;
    [SerializeField] private CoinWorthCollector _coinWorthCollector;
    public Action<Collectible> OnCollectibleCollected { get; set; }
    

    public BaseCollectCommand CollectCommand
    {
        get => _collectCommand;
        set => _collectCommand = value;
    }


    private bool _isTransformed = false;

    private void Awake()
    {
        _collectibleDetector.OnDetected += OnDetected;
    }

    private void OnApplicationQuit()
    {
        _collectibleDetector.OnDetected -= OnDetected;

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
        var worthDefiner = collectible.GetComponent<CoinWorthDefiner>();
        _coinWorthCollector.CollectWorth(new CoinWorth(worthDefiner.Coin,worthDefiner.Count));
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