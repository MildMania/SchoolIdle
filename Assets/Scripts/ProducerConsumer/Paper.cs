using System.Collections;
using UnityEngine;

public class Paper : MonoBehaviour, IProducible, IConsumable
{
    private float _moveDuration = 0.5f;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public void MoveProducible(Transform target, Transform container)
    {
        StopAllCoroutines();
        StartCoroutine(MoveRoutine(target, container));
    }


    public void MoveConsumable(Transform target, Transform container)
    {
        StopAllCoroutines();
        StartCoroutine(MoveRoutine(target, container));
    }

    private IEnumerator MoveRoutine(Transform target, Transform container)
    {
        float currentTime = 0;


        var producibleTransform = transform;
        Vector3 position = producibleTransform.position;


        while (currentTime < _moveDuration)
        {
            float step = currentTime / _moveDuration;

            producibleTransform.position = Vector3.Lerp(position,
                target.position, step);


            currentTime += Time.deltaTime;
            yield return null;
        }

        producibleTransform.position = target.position;
        producibleTransform.SetParent(container);
    }
}