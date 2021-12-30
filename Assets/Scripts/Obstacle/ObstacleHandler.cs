using System;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleHandler : MonoBehaviour
{
    [SerializeField] private BaseUncollectCommand _uncollectCommand;
    [SerializeField] private CollectibleController _collectibleController;
    private BaseObstacleDetector[] _obstacleDetectors;
    private List<Collectible> _collectedCollectibles;
    private BaseUncollectCommand _uncollectCommandClone;

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

        if (_uncollectCommandClone != null)
        {
            _uncollectCommandClone.StopExecution();
        }
    }

    private void OnDetected(Obstacle obstacle)
    {
        if (_uncollectCommand != null)
        {
            CreateCommand();
        }

        Collectible collectible = GetLastCollectible();
        if (collectible != null)
        {
            collectible.OnUncollected += OnUncollected;
            collectible.TryUncollect(_uncollectCommand);
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

    private void CreateCommand()
    {
        _uncollectCommandClone = Instantiate(_uncollectCommand);
        _uncollectCommandClone.CollectedCollectibles = _collectedCollectibles;
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