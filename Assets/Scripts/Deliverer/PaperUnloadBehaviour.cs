using UnityEngine;

public class PaperUnloadBehaviour : BaseUnloadBehaviour<PaperConsumer, Paper>
{
    [SerializeField] private PaperConsumerFovController _paperConsumerFovController;
    [SerializeField] private float _unloadDelay = .3f;


    private void Awake()
    {
        _paperConsumerFovController.OnTargetEnteredFieldOfView += OnPaperConsumerEnteredFieldOfView;
        _paperConsumerFovController.OnTargetExitedFieldOfView += OnPaperConsumerExitedFieldOfView;
    }


    private void OnDestroy()
    {
        _paperConsumerFovController.OnTargetEnteredFieldOfView -= OnPaperConsumerEnteredFieldOfView;
        _paperConsumerFovController.OnTargetExitedFieldOfView -= OnPaperConsumerExitedFieldOfView;


        StopAllCoroutines();
    }

    private void OnPaperConsumerEnteredFieldOfView(PaperConsumer paperConsumer)
    {
        _consumers.Add(paperConsumer);
    }

    private void OnPaperConsumerExitedFieldOfView(PaperConsumer paperConsumer)
    {
        _consumers.Remove(paperConsumer);
    }


    public override void UnloadCustomActions(int index)
    {
        Paper paper = _deliverer.Papers[_deliverer.Papers.Count - 1];
        _deliverer.Papers.Remove(paper);
        _updatedFormationController.RemoveAndGetLastTransform();
        _consumers[index].Consume(paper);
    }
}