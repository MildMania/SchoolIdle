using System;
using System.Collections.Generic;
using UnityEngine;


public class TutorialController : Singleton<TutorialController>
{
	[SerializeField] private List<Transform> TargetList;

	[SerializeField] private Transform _tutorialIndicator; 

	[SerializeField] private PaperLoadBehaviour _paperLoadBehaviour;

	[SerializeField] private PaperUnloadBehaviour _paperUnloadBehaviour;

	private Upgradable _upgradable;

	private void Start()
	{
		_upgradable = UpgradableManager.Instance.GetUpgradable(EAttributeCategory.TUTORIAL,EUpgradable.TUTORIAL);
		if (_upgradable.UpgradableTrackData.Level == 1)
		{
			_tutorialIndicator.gameObject.SetActive(false);
			return;
		}
		_paperLoadBehaviour.OnFistLoaded += OnFirstloaded;
		_paperUnloadBehaviour.OnFistUnloaded += OnFirstUnloaded;
	}
	
	private void OnFirstloaded()
	{
		_tutorialIndicator.position = TargetList[1].position;
	}

	private void OnFirstUnloaded()
	{
		_upgradable.TryUpgrade();
		_tutorialIndicator.gameObject.SetActive(false);
	}
}