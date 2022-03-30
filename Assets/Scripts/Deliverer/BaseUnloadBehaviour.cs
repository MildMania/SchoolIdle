using System.Collections;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseUnloadBehaviour : MonoBehaviour
{
    [SerializeField] protected UpdatedFormationController _updatedFormationController;
    [SerializeField] protected Deliverer _deliverer;
    [SerializeField] protected Upgradable _unloadSpeedUpgradable;

    protected float _unloadDelay;
}

public abstract class BaseUnloadBehaviour<TBaseConsumer, TResource> : BaseUnloadBehaviour
    where TBaseConsumer : BaseConsumer<TResource>
    where TResource : IResource
{
    protected List<TBaseConsumer> _consumers = new List<TBaseConsumer>();

    private void Awake()
    {
        _unloadSpeedUpgradable.OnUpgraded += OnUnloadSpeedUpgraded;

        OnAwakeCustomActions();
    }

    protected virtual void OnAwakeCustomActions()
    {
    }

    private void OnDestroy()
    {
        _unloadSpeedUpgradable.OnUpgraded -= OnUnloadSpeedUpgraded;

        OnDestroyCustomActions();
    }

    protected virtual void OnDestroyCustomActions()
    {
    }

    private void OnUnloadSpeedUpgraded(UpgradableTrackData upgradableTrackData)
    {
        float value =
            GameConfigManager.Instance.GetAttributeUpgradeValue(EAttributeCategory.CHARACTER, upgradableTrackData);

        _unloadDelay = 1 / value;
    }

    protected void OnConsumerEnteredFieldOfView(TBaseConsumer producer)
    {
        _consumers.Add(producer);
    }

    protected void OnConsumerExitedFieldOfView(TBaseConsumer producer)
    {
        _consumers.Remove(producer);
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
                if (_consumers.Count > 0)
                {
                    int index = (int) Random.Range(0, _consumers.Count - 0.1f);
                    if (_deliverer.Resources.Count > 0)
                    {
                        UnloadCustomActions(index);
                        _deliverer.OnContainerEmpty?.Invoke(_deliverer.Container.childCount == 0);
                    }
                }

                currentTime = 0;
            }

            yield return null;
        }
    }


    public abstract void UnloadCustomActions(int index);
}