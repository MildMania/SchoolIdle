using System.Collections.Generic;
using UnityEngine;

public class PaperConsumer : BaseConsumer<Paper>
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;
    [SerializeField] private DelivererFovController _delivererFovController;

    private List<Deliverer> _deliverers = new List<Deliverer>();
    public List<Deliverer> Deliverers => _deliverers;

    private void Awake()
    {
        _delivererFovController.OnTargetEnteredFieldOfView += OnTargetEnteredFieldOfView;
        _delivererFovController.OnTargetExitedFieldOfView += OnTargetExitedFieldOfView;
    }


    private void OnDestroy()
    {
        _delivererFovController.OnTargetEnteredFieldOfView -= OnTargetEnteredFieldOfView;
        _delivererFovController.OnTargetExitedFieldOfView -= OnTargetExitedFieldOfView;
    }

    private void OnTargetEnteredFieldOfView(Deliverer deliverer)
    {
        _deliverers.Add(deliverer);
    }

    private void OnTargetExitedFieldOfView(Deliverer deliverer)
    {
        _deliverers.Remove(deliverer);
    }

    public override void ConsumeCustomActions(Paper paper)
    {
        if (_deliverers.Count > 0)
        {
            int index = (int) Random.Range(0, _deliverers.Count - 0.1f);

            UpdatedFormationController formationController = _deliverers[index].FormationController;
            Transform container = formationController.Container;
            Transform lastPaper = formationController.GetLastTransform();

            if (lastPaper == null)
            {
                return;
            }

            Transform targetTransform = _updatedFormationController.GetLastTargetTransform(paper.transform);

            lastPaper.GetComponent<Paper>().MoveConsumable(targetTransform, container);
        }
    }
}