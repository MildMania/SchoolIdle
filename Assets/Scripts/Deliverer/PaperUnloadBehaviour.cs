using UnityEngine;

public class PaperUnloadBehaviour : BaseUnloadBehaviour<PaperConsumer, Paper>
{
    [SerializeField] private PaperConsumerFovController _paperConsumerFovController;


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
        //Remove from self
        Paper paper = _deliverer.Papers[_deliverer.Papers.Count - 1];
        _deliverer.Papers.Remove(paper);
        _updatedFormationController.RemoveAndGetLastTransform();

        //Add To Consumer
        PaperConsumer paperConsumer = _consumers[index];
        UpdatedFormationController consumerFormationController =
            paperConsumer.GetComponentInChildren<UpdatedFormationController>();
        Transform targetTransform = consumerFormationController.GetLastTargetTransform(paper.transform);
        paper.Move(targetTransform, consumerFormationController.Container);
        paperConsumer.ResourceProvider.Resources.Add(paper);
    }
}