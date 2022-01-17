using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    [SerializeField] private BaseUncollectCommand _uncollectCommand;
    [SerializeField] private CollectibleController _collectibleController;

    private BaseObstacleDetector[] _obstacleDetectors;
    private List<Collectible> _collectedCollectibles;
    public Action<Obstacle> OnObstacleCollided { get; set; }

    public BaseObstacleDetector[] ObstacleDetectors
    {
        get
        {
            if (_obstacleDetectors == null)
            {
                _obstacleDetectors = GetComponentsInChildren<BaseObstacleDetector>();
            }

            return _obstacleDetectors;
        }
    }

    private void Awake()
    {
        _collectedCollectibles = _collectibleController.CollectedCollectibles;

        foreach (var obstacleDetector in ObstacleDetectors)
        {
            obstacleDetector.OnDetected += OnDetected;
        }
    }

    private void OnApplicationQuit()
    {
        foreach (var obstacleDetector in ObstacleDetectors)
        {
            obstacleDetector.OnDetected -= OnDetected;
        }
    }

    private void OnDetected(Obstacle obstacle)
    {
        BaseUncollectCommand uncollectCommandClone = null;
        if (_uncollectCommand != null)
        {
            uncollectCommandClone = CreateCommand();
        }

        Collectible collectible = GetLastCollectible();
        if (collectible != null)
        {
            collectible.OnUncollected += OnUncollected;
            collectible.TryUncollect(uncollectCommandClone);
        }

        obstacle.OnCollided += OnCollided;
        obstacle.TryCollide();
    }

    private void OnUncollected(Collectible collectible)
    {
        collectible.OnUncollected -= OnUncollected;
        _collectedCollectibles.Remove(collectible);
    }


    private void OnCollided(Obstacle obstacle)
    {
        OnObstacleCollided?.Invoke(obstacle);
    }

    private BaseUncollectCommand CreateCommand()
    {
        BaseUncollectCommand uncollectCommandClone = Instantiate(_uncollectCommand);
        uncollectCommandClone.CollectedCollectibles = _collectedCollectibles;
        return uncollectCommandClone;
    }

    private Collectible GetLastCollectible()
    {
        Collectible collectible = null;
        if (_collectedCollectibles.Count > 0)
        {
            collectible = _collectedCollectibles[_collectedCollectibles.Count - 1];
        }

        return collectible;
    }
}