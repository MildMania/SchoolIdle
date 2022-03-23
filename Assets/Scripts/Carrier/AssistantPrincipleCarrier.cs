using UnityEngine;

public class AssistantPrincipleCarrier : CarrierBase
{

    [SerializeField] private Upgradable _characterCapacityUpgradable;

    protected override void OnAwakeCustomActions()
    {
        base.OnAwakeCustomActions();

        _characterCapacityUpgradable.OnUpgraded += OnCharacterCapacityUpgraded;
    }

    private void OnCharacterCapacityUpgraded(UpgradableTrackData upgradableTrackData)
    {
        float value = GameConfigManager.Instance.GetAttributeUpgradeValue(EAttributeCategory.CHARACTER, upgradableTrackData);
        
        UpdateCarryCapacity((int) value);
    }

    protected override void OnDestroyCustomActions()
    {
        base.OnDestroyCustomActions();
        
        _characterCapacityUpgradable.OnUpgraded -= OnCharacterCapacityUpgraded;
    }
}