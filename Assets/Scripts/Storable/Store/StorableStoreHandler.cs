using UnityEngine;

public abstract class StorableStoreHandler : MonoBehaviour
{
	[SerializeField] protected StorableController _storableController;
	[SerializeField] protected StorableFormationController _storableFormationController;

	[SerializeField] private StoreCommandBase _storeCommand;

	private StorableBase _storable;

	protected void StoreStorable(StorableBase storable)
	{
		_storable = storable;

		storable.OnStored += OnStored;

		StoreCommandBase storeCommandClone = CreateStoreCommand();
		storable.Store(storeCommandClone);
	}

	private void OnStored(StorableBase storable)
	{
		_storable.OnStored -= OnStored;
	}

	public StoreCommandBase CreateStoreCommand()
	{
		StoreCommandBase storeCommand = Instantiate(_storeCommand);
		storeCommand.StorableList = _storableController.StorableList;
		storeCommand.ParentTransform = _storableController.StorableContainer;
		storeCommand.TargetTransforms = _storableFormationController.TargetTransforms;

		return storeCommand;
	}
}