using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig",
    menuName = "GameConfig/Create a Game Config",
    order = 1)]
public class GameConfig : SerializedScriptableObject
{
    [Header("ATTRIBUTE UPGRADES")] [OdinSerialize]
    private List<AttributeUpgrade> _attributeUpgrades;

    [Header("ATTRIBUTE REQUIREMENTS")] [OdinSerialize]
    private List<AttributeRequirement> _attributeRequirements;

    public List<AttributeUpgrade> AttributeUpgrades => _attributeUpgrades;
    public List<AttributeRequirement> AttributeRequirements => _attributeRequirements;
    
}

[Serializable]
public struct AttributeUpgrade
{
    [OdinSerialize] public EAttributeCategory _attributeCategoryType;
    [OdinSerialize] public Dictionary<EUpgradable, AttributeInfo[]> Attributes;
}

[Serializable]
public struct AttributeRequirement
{
    [OdinSerialize] public EAttributeCategory _attributeCategoryType;
    [OdinSerialize] public Dictionary<EUpgradable, RequirementInfo[]> Requirements;
}

[Serializable]
public struct AttributeInfo
{
    [OdinSerialize] public int Level { get; set; }
    [OdinSerialize] public float Value { get; set; }
}

[Serializable]
public struct RequirementInfo
{
    [OdinSerialize] public int Level { get; set; }
    [OdinSerialize] public int Value { get; set; }
}

public enum EAttributeCategory
{
    NONE = 0,
    CHARACTER = 1,
    PAPER_DELIVERER_HELPER = 2,
    CLASSROOM_1 = 3,
    CLASSROOM_2 = 4,
}