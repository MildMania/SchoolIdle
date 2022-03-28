using UnityEngine;

[RequireComponent(typeof(UpgradeAreaFovController))]
public class FovBasedUpgradeAreaDetector : BaseUpgradeAreaDetector
{
    [SerializeField] private UpgradeAreaFovController _upgradeAreaFOVController;

    private void Awake()
    {
        SubscribeToFovController();
        _upgradeAreaFOVController.SetActive(true);
    }

    private void OnDestroy()
    {
        _upgradeAreaFOVController.SetActive(false);
        UnsubscribeFromFovController();
    }

    private void SubscribeToFovController()
    {
        _upgradeAreaFOVController.OnTargetEnteredFieldOfView += OnTargetEnteredFieldOfView;
        _upgradeAreaFOVController.OnTargetExitedFieldOfView += OnTargetExitedFieldOfView;
    }

    private void UnsubscribeFromFovController()
    {
        _upgradeAreaFOVController.OnTargetEnteredFieldOfView -= OnTargetEnteredFieldOfView;
        _upgradeAreaFOVController.OnTargetExitedFieldOfView -= OnTargetExitedFieldOfView;
    }
    

    private void OnTargetEnteredFieldOfView(UpgradeArea upgradeArea)
    {
        LastDetected = upgradeArea;
        OnDetected?.Invoke(upgradeArea);
    }

    private void OnTargetExitedFieldOfView(UpgradeArea upgradeArea)
    {
        OnEnded?.Invoke(upgradeArea);
    }
}