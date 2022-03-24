using UnityEngine;

namespace Producer.Old
{
	public class ProducedStorableCollectBehaviour : MonoBehaviour
	{
		[SerializeField] private BaseCharacterDetector _characterDetector;

		[SerializeField] private StorableFormationController _storableFormationController;

		[SerializeField] private StorableController _storableController;
		private void Awake()
		{
			_characterDetector.OnDetected += OnCharacterDetected;
			_characterDetector.OnEnded += OnCharacterEnded;
		}

		private void OnDestroy()
		{
			_characterDetector.OnDetected -= OnCharacterDetected;
			_characterDetector.OnEnded -= OnCharacterEnded;
		}

		private void OnCharacterDetected(Character character)
		{
			character.GetComponentInChildren<CollectibleCollector>().OnCollectibleCollected += OnCollectibleCollected;
		}

		private void OnCharacterEnded(Character character)
		{
			character.GetComponentInChildren<CollectibleCollector>().OnCollectibleCollected -= OnCollectibleCollected;
		}

		private void OnCollectibleCollected(Collectible collectible)
		{
			var storable = collectible.GetComponent<StorableBase>();
			_storableController.StorableList.Remove(storable);
			_storableFormationController.Reformat();
		}
	}
}