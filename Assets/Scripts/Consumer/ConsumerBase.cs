using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConsumerBase : MonoBehaviour
{
    [SerializeField] protected float _consumeDelayTime;
    [SerializeField] private StorableController _storableController;

    public Action<StorableBase> OnConsumed { get; set; }

    private void Start()
    {
        Consume();
    }

    private void Consume()
    {
        StartCoroutine(ConsumeRoutine());
    }

    private IEnumerator ConsumeRoutine()
    {
        while (true)
        {
            int storableCount = _storableController.StorableList.Count;
            
            if (storableCount == 0)
            {
                yield return null;
                continue;
            }
            
            var lastStorable = _storableController.StorableList[storableCount - 1];

            OnConsumed?.Invoke(lastStorable);

            yield return new WaitForSeconds(_consumeDelayTime);
        }
    }
}
