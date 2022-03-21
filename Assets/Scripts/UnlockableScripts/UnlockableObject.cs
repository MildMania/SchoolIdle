using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class UnlockableObject : SerializedMonoBehaviour, IUnlockable
{
	[OdinSerialize] public Unlockable Unlockable { get; private set; } = new Unlockable();

	[SerializeField] private Guid _guid;
		
	[SerializeField] private GameObject _unlockableGO;
	
	[SerializeField] private BaseCharacterDetector _baseCharacterDetector;
	
	private void Awake()
	{
		_baseCharacterDetector.OnDetected += OnDetected;
	}

	private void Start()
	{
		UnlockableTrackable unlockableTrackable;

		if (UserManager.Instance.LocalUser.GetUserData<UserUnlockableData>().Tracker
			.TryGetSingle(_guid, out unlockableTrackable))
		{
			var unlockableTrackData = unlockableTrackable.TrackData;
			Unlockable.Init(unlockableTrackData);
		}
	}

	private void OnDestroy()
	{
		_baseCharacterDetector.OnDetected -= OnDetected;
	}

	private void OnDetected(Character character)
	{
		if (Unlockable.TryUnlock(UserManager.Instance.LocalUser))
		{
			Debug.Log("Unlockable Object Unlock");
			_unlockableGO.SetActive(true);
		}
	}
}