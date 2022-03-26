using System.Collections;
using UnityEngine;

public class ProductionController<TProducer, TResource> : MonoBehaviour where TProducer : BaseProducer<TResource>
    where TResource : IResource
{
    [SerializeField] protected TProducer _producer;
    [SerializeField] protected TResource _resource;
    [SerializeField] private float _productionDelay = 0.2f;
    [SerializeField] private BaseProductionRequirement[] _productionRequirements;

    protected IEnumerator ProduceRoutine(TResource resource)
    {
        float currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > _productionDelay)
            {
                if (IsAllRequirementsMet())
                {
                    ConsumeAllRequirements();
                    //TODO: Consider waiting all resources to be consumed by listening onConsumed Event!
                    _producer.Produce(resource);
                }

                currentTime = 0;
            }


            yield return null;
        }
    }

    private bool IsAllRequirementsMet()
    {
        foreach (var productionRequirement in _productionRequirements)
        {
            if (!productionRequirement.IsProductionRequirementMet())
            {
                return false;
            }
        }

        return true;
    }

    private void ConsumeAllRequirements()
    {
        foreach (var productionRequirement in _productionRequirements)
        {
            productionRequirement.ConsumeRequirements();
        }
    }
}