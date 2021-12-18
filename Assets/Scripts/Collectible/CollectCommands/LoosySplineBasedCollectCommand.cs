using System;
using System.Collections;
using PathCreation;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "LoosySplineBasedCollectCommand",
    menuName = "ScriptableObjects/LoosySplineBasedCollectCommand",
    order = 1)]
public class LoosySplineBasedCollectCommand : BaseCollectCommand
{
    [SerializeField] private float _lerpTime = 0.25f;
    [SerializeField] private Vector3 _distance;
    [SerializeField] public float _smoothTime = 0.1f;

    private float _velocity = 0;
    private int _row;
    private int _column;


    protected override void ExecuteCustomActions(Collectible collectible, Action onCollectCommandExecuted)
    {
        collectible.Collider.enabled = false;

        _row = CollectedCollectibles.Count / TargetTransforms.Length;
        _column = CollectedCollectibles.Count % TargetTransforms.Length;

        TargetTransforms[_column].Add(collectible.transform);
        CollectedCollectibles.Add(collectible);

        collectible.MoveRoutine = MoveRoutine(collectible);
        CoroutineRunner.Instance.StartCoroutine(collectible.MoveRoutine);


        onCollectCommandExecuted?.Invoke();
    }


    private IEnumerator MoveRoutine(Collectible collectible)
    {
        var collectibleTransform = collectible.transform;

        Quaternion rotation = collectibleTransform.rotation;
        Vector3 position = collectibleTransform.position;

        float currentTime = 0;


        SplineCharacterMovementBehaviour splineCharacterMovementBehaviour =
            CharacterTransform.GetComponentInChildren<SplineCharacterMovementBehaviour>();
        if (splineCharacterMovementBehaviour == null)
        {
            yield break;
        }

        PathCreator pathCreator = splineCharacterMovementBehaviour.PathCreator;

        while (currentTime < _lerpTime)
        {
            float step = currentTime / _lerpTime;


            Vector3 targetPosition =
                pathCreator.path.GetPointAtDistance(splineCharacterMovementBehaviour.DistanceTravelled +
                                                    _distance.magnitude * _row) + Vector3.up;
            Quaternion targetRotation = Quaternion.Euler(0,
                pathCreator.path.GetRotationAtDistance(splineCharacterMovementBehaviour.DistanceTravelled +
                                                       _distance.magnitude * _row)
                    .eulerAngles.y, 0);

            var transformParent = collectibleTransform.parent;
            transformParent.position = Vector3.Lerp(position,
                targetPosition, step);

            transformParent.rotation = Quaternion.Lerp(rotation,
                targetRotation, step);

            currentTime += Time.deltaTime;
            yield return null;
        }


        while (true)
        {
            float targetPositionX = TargetTransforms[_column][_row].localPosition.x;
            var localPosition = collectible.transform.localPosition;
            localPosition =
                new Vector3(Mathf.SmoothDamp(localPosition.x,
                        targetPositionX,
                        ref _velocity, _smoothTime), localPosition.y,
                    localPosition.z);
            var transform = collectible.transform;
            transform.localPosition = localPosition;


            var parentTransform = transform.parent;
            parentTransform.position =
                pathCreator.path.GetPointAtDistance(splineCharacterMovementBehaviour.DistanceTravelled +
                                                    _distance.magnitude * _row) + Vector3.up;

            parentTransform.rotation =
                Quaternion.Euler(0,
                    pathCreator.path.GetRotationAtDistance(splineCharacterMovementBehaviour.DistanceTravelled +
                                                           _distance.magnitude * _row)
                        .eulerAngles.y, 0);
            yield return null;
        }
    }
}