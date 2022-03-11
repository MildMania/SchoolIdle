using UnityEngine;

public class TriggerBasedCharacterDetector : BaseCharacterDetector
{
    [SerializeField] private TriggerObjectHitController _characterHitController;


    private void Awake()
    {
        _characterHitController.OnHitTriggerObject += OnHitTriggerObject;
    }

    private void OnDestroy()
    {
        _characterHitController.OnHitTriggerObject -= OnHitTriggerObject;
    }

    private void OnHitTriggerObject(TriggerObject triggerObject)
    {
        Character character = triggerObject.GetComponentInParent<Character>();

        LastDetected = character;
        OnDetected?.Invoke(character);
    }
}