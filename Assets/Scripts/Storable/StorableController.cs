using System;
using System.Collections.Generic;
using UnityEngine;

public class StorableController : MonoBehaviour
{
	public List<StorableBase> StorableList { get; set; }

	[SerializeField] private Transform _storableContainer;
	public Transform StorableContainer => _storableContainer;

	private StorableBase _lastDropable = null;
	public StorableBase LastDropable => _lastDropable;

	private void Awake()
	{
		StorableList = new List<StorableBase>();
	}

	public bool TryDropForConsume(EStorableType storableType)
	{
		int listCount = StorableList.Count;

		for (int i = listCount - 1; i >= 0; i--)
		{
			if (StorableList[i].StorableType == storableType)
			{
				_lastDropable = StorableList[i];
				return true;
			}
		}

		return false;
	}
}