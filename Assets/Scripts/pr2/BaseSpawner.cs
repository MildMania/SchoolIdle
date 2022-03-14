using System.Collections;
using UnityEngine;


public class BaseSpawner : MonoBehaviour
{
	[SerializeField] private GameObject _spawnPrefab;

	[SerializeField] private Transform _spawnParent;

	[SerializeField] private Quaternion _spawnQuaternion;

	[SerializeField] private Transform _lerpPosition;

	[SerializeField] private float _lerpTime = 0.1f;


	public void SpawnCollectible()
	{
		var spawnedGO = Instantiate(_spawnPrefab, transform.position, _spawnQuaternion, _spawnParent);
		StartCoroutine(SpawnLerpRoutine(spawnedGO, _lerpTime, _lerpPosition.position));
	}

	private IEnumerator SpawnLerpRoutine(GameObject spawnedGO, float lerpTime, Vector3 lerpPosition)
	{
		float currentTime = 0;

		var position = spawnedGO.transform.position;

		while (currentTime < lerpTime)
		{
			float step = currentTime / lerpTime;

			spawnedGO.transform.position = Vector3.Lerp(position,
				lerpPosition, step);

			currentTime += Time.deltaTime;
			yield return null;
		}

		yield return new WaitForSeconds(lerpTime);
	}
}