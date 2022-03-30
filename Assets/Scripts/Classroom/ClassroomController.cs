using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class ClassroomController : SerializedMonoBehaviour
{
    [OdinSerialize] private Upgradable _classroomUpgradable;

    private void Awake()
    {
        _classroomUpgradable.OnUpgraded += OnClassroomUpgraded;
    }

    private void OnClassroomUpgraded(UpgradableTrackData upgradableTrackData)
    {
        var classroomUpgradeHandlers = GetComponentsInChildren<ClassroomUpgradeHandler>();

        foreach (var classroomUpgradeHandler in classroomUpgradeHandlers)
        {
            classroomUpgradeHandler.UpgradeByLevel(upgradableTrackData.Level);
        }
        
        float value = GameConfigManager.Instance.GetAttributeUpgradeValue(EAttributeCategory.CLASSROOM_1, upgradableTrackData);

        var productionControllers = GetComponentsInChildren<ProductionController<MoneyProducer, Money>>();

        foreach (var productionController in productionControllers)
        {
            productionController.UpdateProductionDelay(value);
        }
        
    }

    private void OnDestroy()
    {
        _classroomUpgradable.OnUpgraded -= OnClassroomUpgraded;
    }
}
