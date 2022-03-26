using UnityEngine;

public class PaperLoadBehaviour : BaseLoadBehaviour<PaperProducer, Paper>
{
    [SerializeField] private PaperProducerFovController _paperProducerFovController;


    private void Awake()
    {
        _paperProducerFovController.OnTargetEnteredFieldOfView += OnProducerEnteredFieldOfView;
        _paperProducerFovController.OnTargetExitedFieldOfView += OnProducerExitedFieldOfView;
    }


    private void OnDestroy()
    {
        _paperProducerFovController.OnTargetEnteredFieldOfView -= OnProducerEnteredFieldOfView;
        _paperProducerFovController.OnTargetExitedFieldOfView -= OnProducerExitedFieldOfView;


        StopAllCoroutines();
    }


    public override void LoadCustomActions(Paper resource)
    {
        Transform targetTransform = _updatedFormationController.GetLastTargetTransform(resource.transform);
        resource.Move(targetTransform, _updatedFormationController.Container);
        _deliverer.Papers.Add(resource);
    }
}