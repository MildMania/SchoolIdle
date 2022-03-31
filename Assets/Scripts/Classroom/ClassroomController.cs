using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class ClassroomController : MonoBehaviour
{
    [SerializeField] private EAttributeCategory _attributeCategory;
    [SerializeField] private EUpgradable _speedUpgradableType;
    
    private Upgradable _classroomUpgradable;
    

    private void Start()
    {
        _classroomUpgradable = UpgradableManager.Instance.GetUpgradable(_attributeCategory, _speedUpgradableType);
        
        UpgradeClassroom(_classroomUpgradable.UpgradableTrackData);
        
        _classroomUpgradable.OnUpgraded += OnClassroomUpgraded;
    }

    private void UpgradeClassroom(UpgradableTrackData upgradableTrackData)
    {
        var classroomUpgradeHandlers = GetComponentsInChildren<ClassroomUpgradeHandler>();

        foreach (var classroomUpgradeHandler in classroomUpgradeHandlers)
        {
            classroomUpgradeHandler.UpgradeByLevel(upgradableTrackData.Level);
        }
        
        float value = GameConfigManager.Instance.GetAttributeUpgradeValue( _classroomUpgradable.AttributeCategory, upgradableTrackData);

        var productionControllers = GetComponentsInChildren<ProductionController<MoneyProducer, Money>>();

        foreach (var productionController in productionControllers)
        {
            productionController.UpdateProductionDelay(value);
        }


    }

    private void OnClassroomUpgraded(UpgradableTrackData upgradableTrackData)
    {
       UpgradeClassroom(upgradableTrackData);
    }

    private void OnDestroy()
    {
        _classroomUpgradable.OnUpgraded -= OnClassroomUpgraded;
    }
}
