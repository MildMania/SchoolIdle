using System;
using System.Collections.Generic;
using MMFramework_2._0.PhaseSystem.Core.EventListener;
using UnityEngine;


public class TutorialController : Singleton<TutorialController>
{
	[SerializeField] private List<Transform> TargetList;

	[SerializeField] private Transform _tutorialIndicator;
	[SerializeField] private Transform _characterIndicator;
	

	[SerializeField] private PaperLoadBehaviour _paperLoadBehaviour;

	[SerializeField] private PaperUnloadBehaviour _paperUnloadBehaviour;

	[SerializeField] private FovBasedUpgradeAreaDetector _fovBasedUpgradeAreaDetector;
	
	[SerializeField] private int _requiredCoinCount;

	private Upgradable _upgradable;

	[SerializeField] private CoinController _coinController;
	

	private bool _doOnce = true;

	private void Awake()
	{
		DeactiveObjects();
	}

	private void Start()
	{
		_upgradable = UpgradableManager.Instance.GetUpgradable(EAttributeCategory.TUTORIAL,EUpgradable.TUTORIAL);
	}

	[PhaseListener(typeof(GamePhase), true)]
	private void ActivateObjectOnGamePhase()
	{
		
		if (_upgradable.UpgradableTrackData.Level == 2)
		{
			_doOnce = false;
			return;
		}

		_fovBasedUpgradeAreaDetector.OnDetected += OnFovDetected;
		

		if (_upgradable.UpgradableTrackData.Level == 1)
		{
			return;
		}

		ActivateObjects();
		
		_paperLoadBehaviour.OnFistLoaded += OnFirstloaded;
		_paperUnloadBehaviour.OnFistUnloaded += OnFirstUnloaded;


	}

	private void OnFovDetected(UpgradeArea upgradeArea)
	{
		_upgradable.TryUpgrade();
		
		DeactiveObjects();
	}


	private void OnFirstloaded()
	{
		_tutorialIndicator.position = TargetList[1].position;
	}
	

	private void Update()
	{
		if (_characterIndicator.gameObject.activeSelf)
		{
			_characterIndicator.LookAt(_tutorialIndicator.position);
		}

		if (_coinController.CurrentCoinCount >= _requiredCoinCount && _doOnce && _upgradable.UpgradableTrackData.Level < 2)
		{
			ActivateObjects();
			
			_tutorialIndicator.position = TargetList[2].position;

			_doOnce = false;
		}
		
	}

	private void DeactiveObjects()
	{
		_tutorialIndicator.gameObject.SetActive(false);
		_characterIndicator.gameObject.SetActive(false);
	}

	private void ActivateObjects()
	{
		_tutorialIndicator.gameObject.SetActive(true);
		_characterIndicator.gameObject.SetActive(true);
	}

	private void OnFirstUnloaded()
	{
		_upgradable.TryUpgrade();
		
		DeactiveObjects();
	}
}