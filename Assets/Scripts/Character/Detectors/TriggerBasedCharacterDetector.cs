using UnityEngine;

public class TriggerBasedCharacterDetector : BaseCharacterDetector
{
    [SerializeField] private TriggerObjectHitController _characterHitController;


    private void Awake()
    {
        _characterHitController.OnHitTriggerObject += OnHitTriggerObject;
        _characterHitController.OnHitEndedTriggerObject += OnHitEndedTriggerObject;
    }
    
    private void OnDestroy()
    {
        _characterHitController.OnHitTriggerObject -= OnHitTriggerObject;
        _characterHitController.OnHitEndedTriggerObject -= OnHitEndedTriggerObject;
    }

    private void OnHitTriggerObject(TriggerObject triggerObject)
    {
        Character character = triggerObject.GetComponent<Character>();

        LastDetected = character;
        OnDetected?.Invoke(character);
    }
    
    private void OnHitEndedTriggerObject(TriggerObject triggerObject)
    {
        Character character = triggerObject.GetComponent<Character>();

        LastDetected = character;
        OnEnded?.Invoke(character);
    }
}