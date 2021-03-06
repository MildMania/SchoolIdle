using System;
using System.Collections;
using UnityEngine;

public class Paper : MonoBehaviour, IResource
{
    private float _moveDuration = 0.5f;

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    public Action<IResource> OnMoveRoutineFinished { get; set; }

    public void Move(Transform target, Transform container)
    {
        StartCoroutine(MoveRoutine(target, container));
    }

    private IEnumerator MoveRoutine(Transform target, Transform container)
    {
        float currentTime = 0;


        var producibleTransform = transform;
        Vector3 position = producibleTransform.position;
        producibleTransform.SetParent(container);
        
        while (currentTime < _moveDuration)
        {
            float step = currentTime / _moveDuration;

            producibleTransform.position = Vector3.Lerp(position,
                target.position, step);


            currentTime += Time.deltaTime;
            yield return null;
        }

        producibleTransform.position = target.position;
        
        OnMoveRoutineFinished?.Invoke(this);
    }
}