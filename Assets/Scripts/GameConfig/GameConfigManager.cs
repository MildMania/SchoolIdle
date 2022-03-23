using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfigManager : Singleton<GameConfigManager>
{
    [SerializeField] private GameConfig _gameConfig;

    public GameConfig GameConfig => _gameConfig;
    
    private Dictionary<EUpgradable, AttributeInfo[]> GetAttributes(EAttributeCategory attributeCategoryType)
    {
        Dictionary<EUpgradable, AttributeInfo[]> attributes = new Dictionary<EUpgradable, AttributeInfo[]>();
        
        foreach (var attributeUpgrade in _gameConfig.AttributeUpgrades)
        {
            if (attributeUpgrade._attributeCategoryType == attributeCategoryType)
            {
                attributes = attributeUpgrade.Attributes;
                break;
            }
        }

        return attributes;
    }
    
    private Dictionary<EUpgradable, RequirementInfo[]> GetRequirements(EAttributeCategory attributeCategoryType)
    {
        Dictionary<EUpgradable, RequirementInfo[]> requirements = new Dictionary<EUpgradable, RequirementInfo[]>();
        
        foreach (var attributeRequirement in _gameConfig.AttributeRequirements)
        {
            if (attributeRequirement._attributeCategoryType == attributeCategoryType)
            {
                requirements = attributeRequirement.Requirements;
                break;
            }
        }

        return requirements;
    }
    
    public float GetAttributeUpgradeValue(EAttributeCategory attributeCategoryType, UpgradableTrackData upgradableTrackData)
    {
        float attributeValue = 0f;
        
        Dictionary<EUpgradable, AttributeInfo[]> attributes = GetAttributes(attributeCategoryType);

        if (attributes.Count == 0)
        {
            return attributeValue;
        }

        if (!attributes.ContainsKey(upgradableTrackData.TrackID))
        {
            return attributeValue;;
        }
        
        AttributeInfo[] attributeInfos = attributes[upgradableTrackData.TrackID];
        
        foreach (var attributeInfo in attributeInfos)
        {
            if (attributeInfo.Level == upgradableTrackData.Level)
            {
                attributeValue = attributeInfo.Value;
                break;
            }
        }

        return attributeValue;
    }

    public List<IRequirement> CreateRequirementList(EAttributeCategory attributeCategoryType, EUpgradable upgradableType)
    {
        List<IRequirement> requirementList = new List<IRequirement>();
        
        Dictionary<EUpgradable, RequirementInfo[]> requirements = GetRequirements(attributeCategoryType);

        if (requirements.Count == 0)
        {
            return null;
        }

        if (!requirements.ContainsKey(upgradableType))
        {
            return null;
        }
        
        RequirementInfo[] requirementInfos = requirements[upgradableType];

        foreach (var requirementInfo in requirementInfos)
        {
            RequirementDataCoin dataCoin = new RequirementDataCoin(ECoin.Gold, requirementInfo.Value);
            requirementList.Add(dataCoin.CreateRequirement(UserManager.Instance.LocalUser));
        }
        
        return requirementList;
    }
}
