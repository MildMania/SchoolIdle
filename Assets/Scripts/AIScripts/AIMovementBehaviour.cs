using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class AIMovementBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _rootTransform;

    [SerializeField] private Seeker _seeker;
    [SerializeField] private AIHelperAIPath _aIPath;
    
    [SerializeField] private EAttributeCategory _attributeCategory;
    [SerializeField] private EUpgradable _speedUpgradableType;

    private float _movementSpeed;

    private System.Action _currentPathCompletedCallback;

    private Upgradable _helperSpeedUpgradable;
    
    private void Awake()
    {
        _aIPath.OnPathCompleted += OnPathCompleted;
        
    }

    private void Start()
    {
        _helperSpeedUpgradable = HelperUpgradableManager.Instance.GetUpgradable(_attributeCategory, _speedUpgradableType);

        _movementSpeed = GameConfigManager.Instance.GetAttributeUpgradeValue(_attributeCategory, _helperSpeedUpgradable.UpgradableTrackData);
        _aIPath.maxSpeed = _movementSpeed;
        
        _helperSpeedUpgradable.OnUpgraded += OnUpgradableUpgraded;
    }

    private void OnDestroy()
    {
        _helperSpeedUpgradable.OnUpgraded -= OnUpgradableUpgraded;
    }

    private void OnUpgradableUpgraded(UpgradableTrackData upgradableTrackData)
    {
        float value = GameConfigManager.Instance.GetAttributeUpgradeValue(_attributeCategory, upgradableTrackData);

        _movementSpeed = value;
        _aIPath.maxSpeed = _movementSpeed;
    }

    private IEnumerator MoveRoutine(Vector3 targetPos)
    {
        var path = _seeker.StartPath(_rootTransform.position, targetPos);
        yield return StartCoroutine(path.WaitForPath());
        _aIPath.SetPath(path);
    }

    public void MoveDestination(Vector3 targetPos, System.Action onPathCompletedCallback = null)
    {
        _currentPathCompletedCallback = onPathCompletedCallback;
        StartCoroutine(MoveRoutine(targetPos));
    }

    public void Stop()
    {
        _movementSpeed = 0;
    }

    private void OnPathCompleted()
    {
        if(_currentPathCompletedCallback != null)
            _currentPathCompletedCallback();
    }
}
