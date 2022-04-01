using System;
using System.Collections;
using System.Collections.Generic;
using MMFramework.TasksV2;
using UnityEngine;

public class HelperSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _helperPrefab;
    [SerializeField] private Transform _spawnTarget;

    [SerializeField] private Upgradable _helperHireUpgradable;
    [SerializeField] private EAttributeCategory _attributeCategory;

    private bool _isGameStarted = true;


    private void Start()
    {
        _helperHireUpgradable.OnUpgraded += OnHelperHireUpgraded;
    }

    private void OnHelperHireUpgraded(UpgradableTrackData upgradableTrackData)
    {
        float value;
        
        if (_isGameStarted)
        {
            value = GameConfigManager.Instance.GetAttributeUpgradeTotalValue(_attributeCategory, upgradableTrackData);
            _isGameStarted = false;
        }
        else
        {
            value = GameConfigManager.Instance.GetAttributeUpgradeValue(_attributeCategory, upgradableTrackData);
        }


        StartCoroutine(SpawnHelpersRoutine((int) value));
    }

    private void OnDestroy()
    {
        _helperHireUpgradable.OnUpgraded -= OnHelperHireUpgraded;
    }
    

    private IEnumerator SpawnHelpersRoutine(int spawnCount)
    {
        for (int i = 0; i < spawnCount; i++)
        {
            var helper = Instantiate(_helperPrefab);
            helper.transform.position = _spawnTarget.position;

            yield return null;
        }
    }
}
