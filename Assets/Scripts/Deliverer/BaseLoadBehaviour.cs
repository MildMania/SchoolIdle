using System;
using System.Collections;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseLoadBehaviour : SerializedMonoBehaviour
{
    [SerializeField] protected EAttributeCategory _attributeCategory;
    [SerializeField] protected UpdatedFormationController _updatedFormationController;
    [SerializeField] protected Deliverer _deliverer;
    [SerializeField] protected bool _canLoadUnlimited;

    [SerializeField] protected Transform _container;

    [HideIf("_canLoadUnlimited")] [SerializeField]
    protected Upgradable _loadCapacityUpgradable;

    [SerializeField] protected Upgradable _loadSpeedUpgradable;

    protected int _loadCapacity;

    protected float _loadDelay;

    public Action OnCapacityEmpty;
    public Action OnCapacityFull;

    public virtual void StopLoading()
    {
    }

    //TODO: Consider controlling loading/unloading using start/stop loading methods!
}

public abstract class BaseLoadBehaviour<TBaseProducer, TResource> : BaseLoadBehaviour
    where TBaseProducer : BaseProducer<TResource>
    where TResource : IResource
{
    private List<TBaseProducer> _producers = new List<TBaseProducer>();


    private void Awake()
    {
        if (!_canLoadUnlimited)
        {
            _loadCapacityUpgradable.OnUpgraded += OnLoadCapacityUpgraded;
        }

        _loadSpeedUpgradable.OnUpgraded += OnLoadSpeedUpgraded;

        OnAwakeCustomActions();
    }

    protected virtual void OnAwakeCustomActions()
    {
    }

    private void OnLoadCapacityUpgraded(UpgradableTrackData upgradableTrackData)
    {
        float value =
            GameConfigManager.Instance.GetAttributeUpgradeValue(_attributeCategory, upgradableTrackData);

        _loadCapacity = (int) value;
    }

    private void OnDestroy()
    {
        StopLoading();
    }

    private void OnLoadSpeedUpgraded(UpgradableTrackData upgradableTrackData)
    {
        float value =
            GameConfigManager.Instance.GetAttributeUpgradeValue(_attributeCategory, upgradableTrackData);

        _loadDelay = 1 / value;
    }

    protected virtual void OnDestroyCustomActions()
    {
    }

    protected void OnProducerEnteredFieldOfView(TBaseProducer producer)
    {
        _producers.Add(producer);
    }

    protected void OnProducerExitedFieldOfView(TBaseProducer producer)
    {
        _producers.Remove(producer);
    }

    [PhaseListener(typeof(GamePhase), true)]
    public void OnGamePhaseStarted()
    {
        StartCoroutine(LoadRoutine());
    }

    private bool CanLoad()
    {
        if (_canLoadUnlimited)
        {
            return true;
        }

        Debug.Log("Load Capacity: " + _loadCapacity);

        return _deliverer.Container.childCount < _loadCapacity;
    }

    private IEnumerator LoadRoutine()
    {
        float currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (currentTime > _loadDelay)
            {
                if (_container.childCount == 0)
                    OnCapacityEmpty?.Invoke();
                else if (_container.childCount == _loadCapacity)
                    OnCapacityFull?.Invoke();

                if (_producers.Count > 0 && CanLoad())
                {
                    int index = (int) Random.Range(0, _producers.Count - 0.1f);

                    TResource resource = default(TResource);
                    if (_producers[index].TryRemoveAndGetLastResource(ref resource))
                    {
                        LoadCustomActions(resource);
                        _deliverer.OnContainerEmpty?.Invoke(_deliverer.Container.childCount == 0);
                    }
                }

                currentTime = 0;
            }

            yield return null;
        }
    }


    public override void StopLoading()
    {
        base.StopLoading();
        if (!_canLoadUnlimited)
        {
            _loadCapacityUpgradable.OnUpgraded -= OnLoadCapacityUpgraded;
        }

        _loadSpeedUpgradable.OnUpgraded -= OnLoadSpeedUpgraded;

        OnDestroyCustomActions();
    }

    public abstract void LoadCustomActions(TResource resource);
}