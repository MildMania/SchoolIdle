using System;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

public class UnlockableObject : SerializedMonoBehaviour, IUnlockable
{
	[OdinSerialize] public Unlockable Unlockable { get; private set; } = new Unlockable();

	[SerializeField] private Guid _guid;
		
	[SerializeField] private GameObject _unlockableGO;
	
	[SerializeField] private GameObject _lockObjects;

	[SerializeField] private BaseCharacterDetector _baseCharacterDetector;

	private UnlockableTrackData _unlockableTrackData;
	public Action<UnlockableTrackData> OnUnlockableInit;
	public Action<UnlockableTrackData> OnTryUnlock;
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
			_unlockableTrackData = unlockableTrackable.TrackData;
		}
		else
		{
			_unlockableTrackData = new UnlockableTrackData(_guid, 0, false);
			unlockableTrackable = new UnlockableTrackable(_unlockableTrackData);
			UserManager.Instance.LocalUser.GetUserData<UserUnlockableData>().Tracker.TryCreate(unlockableTrackable);
		}
		
		Unlockable.Init(_unlockableTrackData);
		OnUnlockableInit?.Invoke(_unlockableTrackData);
		_unlockableGO.SetActive(_unlockableTrackData.IsUnlock);
		if (_lockObjects != null)
		{
			_lockObjects.SetActive(!_unlockableTrackData.IsUnlock);
		}
		gameObject.SetActive(!_unlockableTrackData.IsUnlock);
		
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
			if (_lockObjects != null)
			{
				_lockObjects.SetActive(false);
			}
		}
		OnTryUnlock?.Invoke(_unlockableTrackData);

		var coinController = character.GetComponentInChildren<CoinController>();
		coinController.UpdateCoinCount();
		
		UserManager.Instance.LocalUser.SaveData(onSavedCallback);
		void onSavedCallback()
		{
			Debug.Log("SAVED!!!");
		}
	}
}