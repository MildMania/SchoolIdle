using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeCard : MonoBehaviour
{
    [SerializeField] private Upgradable _upgradable;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _valueText;

    private void Awake()
    {
        _upgradable.OnUpgraded += OnUpgradableUpgraded;
    }

    private void OnUpgradableUpgraded(UpgradableTrackData upgradableTrackData)
    {
        RequirementInfo nextRequirementInfo= GameConfigManager.Instance.GetNextRequirementInfo(_upgradable.AttributeCategory, upgradableTrackData);
        
        if (nextRequirementInfo.Level == -1)
        {
            Debug.Log("UPGRADED LEVEL: " + nextRequirementInfo.Level + " VALUE: " +nextRequirementInfo.Value);
            _valueText.text = "MAX";
        }
        else
        {
            Debug.Log("UPGRADED LEVEL: " + nextRequirementInfo.Level + " VALUE: " +nextRequirementInfo.Value);
            _valueText.text = nextRequirementInfo.Value.ToString();
        }
    }

    private void OnDestroy()
    {
        _upgradable.OnUpgraded -= OnUpgradableUpgraded;
    }

    private void Start()
    {
        RequirementInfo nextRequirementInfo = GameConfigManager.Instance.GetNextRequirementInfo(_upgradable.AttributeCategory, _upgradable.UpgradableTrackData);

        if (nextRequirementInfo.Level == -1)
        {
            Debug.Log("LEVEL: " + nextRequirementInfo.Level + " VALUE: " +nextRequirementInfo.Value);

            _valueText.text = "MAX";
        }
        else
        {
            Debug.Log("LEVEL: " + nextRequirementInfo.Level + " VALUE: " +nextRequirementInfo.Value);
            _valueText.text = nextRequirementInfo.Value.ToString();
        }
    }
}
