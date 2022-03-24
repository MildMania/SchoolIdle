using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PaperProducer : BaseProducer<Paper>
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;
    [SerializeField] private DelivererFovController _delivererFovController;


    private List<Deliverer> _deliverers = new List<Deliverer>();

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

    public override void ProduceCustomActions(Paper paper)
    {
        Transform targetTransform = null;
        if (_deliverers.Count > 0)
        {
            int index = (int) Random.Range(0, _deliverers.Count - 0.1f);
            targetTransform = _deliverers[index].transform;
        }
        else
        {
            targetTransform = _updatedFormationController.GetLastTargetTransform(_producibles, paper.transform);
        }

        Paper clonedPaperProducible = Instantiate(paper, paper.transform.position, paper.transform.rotation);
        clonedPaperProducible.MoveProducible(targetTransform);
    }
}