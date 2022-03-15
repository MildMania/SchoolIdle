using UnityEngine;

public class StorableConsumerStoreHandler : StorableStoreHandler
{
	[SerializeField] private BaseCharacterDetector _baseCharacterDetector;


	private void Awake()
	{
		_baseCharacterDetector.OnDetected += OnCharacterDetected;
		_baseCharacterDetector.OnEnded += OnCharacterEnded;
	}


	private void OnDestroy()
	{
		_baseCharacterDetector.OnDetected -= OnCharacterDetected;
		_baseCharacterDetector.OnEnded -= OnCharacterEnded;
	}

	private void OnCharacterDetected(Character character)
	{
		var storableDropHandler = character.GetComponentInChildren<StorableDropHandler>();

		storableDropHandler.OnStorableDropped += OnStorableDropped;
		storableDropHandler.StartDrop();
	}

	private void OnStorableDropped(StorableBase storable)
	{
		StoreStorable(storable);
	}

	private void OnCharacterEnded(Character character)
	{
		var storableDropHandler = character.GetComponentInChildren<StorableDropHandler>();

		storableDropHandler.StopDrop();
		storableDropHandler.OnStorableDropped -= OnStorableDropped;
	}
}