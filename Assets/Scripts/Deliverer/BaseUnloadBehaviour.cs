using System;
using System.Collections;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class BaseUnloadBehaviour : MonoBehaviour
{
    [SerializeField] protected UpdatedFormationController _updatedFormationController;
    [SerializeField] protected Deliverer _deliverer;
    protected Upgradable _unloadSpeedUpgradable;

    [SerializeField] protected EAttributeCategory _attributeCategory;
    [SerializeField] protected EUpgradable _unloadSpeedUpgradableType;

    [SerializeField] protected bool _isActiveOnStart = true;

    [SerializeField] protected iOSHapticFeedback.iOSFeedbackType _hapticType = iOSHapticFeedback.iOSFeedbackType.ImpactMedium;

    protected OnHapticRequestedEventRaiser _onHapticRequestedEventRaiser = new OnHapticRequestedEventRaiser();


    protected float _unloadDelay;

    protected bool _isActive = true;

    public void StopUnloading()
    {
        StopUnloadingCustomActions();
    }

    public abstract void StopUnloadingCustomActions();

    protected abstract IEnumerator UnloadRoutine();

    public void Activate()
    {
        _isActive = true;
        StartCoroutine(UnloadRoutine());
    }

    public void Deactivate()
    {
        _isActive = false;
        StopAllCoroutines();
    }
}

public abstract class BaseUnloadBehaviour<TBaseConsumer, TResource> : BaseUnloadBehaviour
    where TBaseConsumer : BaseConsumer<TResource>
    where TResource : IResource
{
    protected List<TBaseConsumer> _consumers = new List<TBaseConsumer>();


    private void Awake()
    {
        OnAwakeCustomActions();
    }

    private void Start()
    {
        _unloadSpeedUpgradable = UpgradableManager.Instance.GetUpgradable(_attributeCategory, _unloadSpeedUpgradableType);

        _unloadDelay = 1 / GameConfigManager.Instance.GetAttributeUpgradeValue(_attributeCategory, _unloadSpeedUpgradable.UpgradableTrackData);
        
        _unloadSpeedUpgradable.OnUpgraded += OnUnloadSpeedUpgraded;

        if(_isActiveOnStart)
            Activate();        
    }
    
    protected virtual void OnAwakeCustomActions()
    {
    }

    private void OnDestroy()
    {
        StopUnloading();
    }

    protected virtual void OnDestroyCustomActions()
    {
        Deactivate();
    }

    private void OnUnloadSpeedUpgraded(UpgradableTrackData upgradableTrackData)
    {
        float value = GameConfigManager.Instance.GetAttributeUpgradeValue(_attributeCategory, upgradableTrackData);

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
    

    protected override IEnumerator UnloadRoutine()
    {
        float currentTime = 0;

        while (true)
        {
            currentTime += Time.deltaTime;

            if (_isActive && currentTime > _unloadDelay)
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


    public override void StopUnloadingCustomActions()
    {
        if (_unloadSpeedUpgradable != null)
            _unloadSpeedUpgradable.OnUpgraded -= OnUnloadSpeedUpgraded;

        OnDestroyCustomActions();
    }

    public abstract void UnloadCustomActions(int index);
}