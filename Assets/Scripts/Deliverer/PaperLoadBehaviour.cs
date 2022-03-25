using UnityEngine;

public class PaperLoadBehaviour : BaseLoadBehaviour<PaperProducer, Paper>
{
    [SerializeField] private PaperProducerFovController _paperProducerFovController;
    [SerializeField] private float _loadDelay = .3f;


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


    public override void LoadCustomActions(Paper paper)
    {
        Transform targetTransform = _updatedFormationController.GetLastTargetTransform(paper.transform);
        paper.MoveProducible(targetTransform, _updatedFormationController.Container);
        _deliverer.Papers.Add(paper);
    }
}