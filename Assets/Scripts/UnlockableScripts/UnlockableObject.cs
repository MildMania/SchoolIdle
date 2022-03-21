using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class UnlockableObject : SerializedMonoBehaviour, IUnlockable
{
	[OdinSerialize] public Unlockable Unlockable { get; private set; } = new Unlockable();

	[SerializeField] private Guid _guid;
		
	[SerializeField] private GameObject _unlockableGO;
	[SerializeField] private GameObject _triggerGO;

	[SerializeField] private BaseCharacterDetector _baseCharacterDetector;
	
	private void Awake()
	{
		_baseCharacterDetector.OnDetected += OnDetected;
	}

	private void Start()
	{
		UnlockableTrackable unlockableTrackable;
		UnlockableTrackData unlockableTrackData;

		if (UserManager.Instance.LocalUser.GetUserData<UserUnlockableData>().Tracker
			.TryGetSingle(_guid, out unlockableTrackable))
		{
			unlockableTrackData = unlockableTrackable.TrackData;
		}
		else
		{
			unlockableTrackData = new UnlockableTrackData(_guid, 0, false);
			unlockableTrackable = new UnlockableTrackable(unlockableTrackData);
			UserManager.Instance.LocalUser.GetUserData<UserUnlockableData>().Tracker.TryCreate(unlockableTrackable);
		}
		
		Unlockable.Init(unlockableTrackData);
		
		_unlockableGO.SetActive(unlockableTrackData.IsUnlock);
		gameObject.SetActive(!unlockableTrackData.IsUnlock);
		
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
			gameObject.SetActive(false);
			
		}
		
		UserManager.Instance.LocalUser.SaveData(onSavedCallback);
		void onSavedCallback()
		{
			Debug.Log("SAVED!!!");
		}
	}
}