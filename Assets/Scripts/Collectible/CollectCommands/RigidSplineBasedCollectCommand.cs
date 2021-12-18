using System;
using System.Collections;
using PathCreation;
using UnityEngine;

[CreateAssetMenu(fileName = "RigidSplineBasedCollectCommand",
    menuName = "ScriptableObjects/RigidSplineBasedCollectCommand",
    order = 1)]
public class RigidSplineBasedCollectCommand : BaseCollectCommand
{
    [SerializeField] private float _lerpTime = 0.25f;
    [SerializeField] private Vector3 _distance;

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
        PathCreator pathCreator = splineCharacterMovementBehaviour.PathCreator;
        int index = CollectedCollectibles.Count;

        while (currentTime < _lerpTime)
        {
            float step = currentTime / _lerpTime;


            Vector3 targetPosition =
                pathCreator.path.GetPointAtDistance(splineCharacterMovementBehaviour.DistanceTravelled +
                                                    _distance.magnitude * index) + Vector3.up;
            Quaternion targetRotation = Quaternion.Euler(0,
                pathCreator.path.GetRotationAtDistance(splineCharacterMovementBehaviour.DistanceTravelled +
                                                       _distance.magnitude * index)
                    .eulerAngles.y, 0);

            var parentTransform = collectibleTransform.parent;
            parentTransform.position = Vector3.Lerp(position,
                targetPosition, step);

            parentTransform.rotation = Quaternion.Lerp(rotation,
                targetRotation, step);

            currentTime += Time.deltaTime;
            yield return null;
        }


        while (true)
        {
            collectible.transform.localPosition = TargetTransforms[_column][_row].localPosition;


            var parentTransform = collectible.transform.parent;
            parentTransform.position =
                pathCreator.path.GetPointAtDistance(splineCharacterMovementBehaviour.DistanceTravelled +
                                                    _distance.magnitude * index) + Vector3.up;

            parentTransform.rotation =
                Quaternion.Euler(0,
                    pathCreator.path.GetRotationAtDistance(splineCharacterMovementBehaviour.DistanceTravelled +
                                                           _distance.magnitude * index)
                        .eulerAngles.y, 0);
            yield return null;
        }
    }
}