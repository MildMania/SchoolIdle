using System.Collections;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;

public class PaperLoadBehaviour : MonoBehaviour
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;
    [SerializeField] private PaperProducerFovController _paperProducerFovController;
    [SerializeField] private Deliverer _deliverer;
    [SerializeField] private float _loadDelay = .3f;

    private List<PaperProducer> _paperProducers = new List<PaperProducer>();


    private void Awake()
    {
        _paperProducerFovController.OnTargetEnteredFieldOfView += OnPaperProducerEnteredFieldOfView;
        _paperProducerFovController.OnTargetExitedFieldOfView += OnPaperProducerExitedFieldOfView;
    }


    private void OnDestroy()
    {
        _paperProducerFovController.OnTargetEnteredFieldOfView -= OnPaperProducerEnteredFieldOfView;
        _paperProducerFovController.OnTargetExitedFieldOfView -= OnPaperProducerExitedFieldOfView;


        StopAllCoroutines();
    }

    private void OnPaperProducerEnteredFieldOfView(PaperProducer paperProducer)
    {
        _paperProducers.Add(paperProducer);
    }

    private void OnPaperProducerExitedFieldOfView(PaperProducer paperProducer)
    {
        _paperProducers.Remove(paperProducer);
    }

    [PhaseListener(typeof(GamePhase), true)]
    public void OnGamePhaseStarted()
    {
        StartCoroutine(LoadRoutine());
    }

    private IEnumerator LoadRoutine()
    {
        float currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > _loadDelay)
            {
                if (_paperProducers.Count > 0)
                {
                    int index = (int) Random.Range(0, _paperProducers.Count - 0.1f);
                    Paper paper = null;
                    if (_paperProducers[index].TryRemoveAndGetLastProducible(ref paper))
                    {
                        Transform targetTransform = _updatedFormationController.GetLastTargetTransform(paper.transform);
                        paper.MoveProducible(targetTransform, _updatedFormationController.Container);
                        _deliverer.Papers.Add(paper);
                    }
                }

                currentTime = 0;
            }

            yield return null;
        }
    }
}