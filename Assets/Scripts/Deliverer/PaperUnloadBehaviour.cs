using System.Collections;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;

public class PaperUnloadBehaviour : MonoBehaviour
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;
    [SerializeField] private PaperConsumerFovController _paperConsumerFovController;
    [SerializeField] private Deliverer _deliverer;
    [SerializeField] private float _unloadDelay = .3f;

    private List<PaperConsumer> _paperConsumers = new List<PaperConsumer>();


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
        _paperConsumers.Add(paperConsumer);
    }

    private void OnPaperConsumerExitedFieldOfView(PaperConsumer paperConsumer)
    {
        _paperConsumers.Remove(paperConsumer);
    }

    [PhaseListener(typeof(GamePhase), true)]
    public void OnGamePhaseStarted()
    {
        StartCoroutine(UnloadRoutine());
    }

    private IEnumerator UnloadRoutine()
    {
        float currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > _unloadDelay)
            {
                if (_paperConsumers.Count > 0)
                {
                    int index = (int) Random.Range(0, _paperConsumers.Count - 0.1f);

                    if (_deliverer.Papers.Count > 0)
                    {
                        Paper paper = _deliverer.Papers[_deliverer.Papers.Count - 1];
                        _deliverer.Papers.Remove(paper);
                        _updatedFormationController.RemoveAndGetLastTransform();
                        _paperConsumers[index].Consume(paper);
                    }
                    //TODO: Check other unloadable varients (like if money stacked between paper piles)
                }

                currentTime = 0;
            }

            yield return null;
        }
    }
}