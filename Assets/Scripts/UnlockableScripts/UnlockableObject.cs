using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class UnlockableObject : SerializedMonoBehaviour, IUnlockable
{
	[OdinSerialize] public Unlockable Unlockable { get; private set; } = new Unlockable();

	[SerializeField] private GameObject _unlockableGO;
	
	[SerializeField] private BaseCharacterDetector _baseCharacterDetector;
	
	private void Awake()
	{
		_baseCharacterDetector.OnDetected += OnDetected;
	}

	private void OnDestroy()
	{
		_baseCharacterDetector.OnDetected -= OnDetected;
	}

	private void OnDetected(Character character)
	{
		if (Unlockable.TrySetLocked(UserManager.Instance.LocalUser, false))
		{
			Debug.Log("Unlockable Object Unlock");
			_unlockableGO.SetActive(true);
		}
		
	}
}