using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "RigidCollectCommand", menuName = "ScriptableObjects/RigidCollectCommand",
    order = 1)]
public class RigidCollectCommand : BaseCollectCommand
{
    [SerializeField] private float _lerpTime = 0.25f;
    [SerializeField] private Vector3 _distance;

    private ParentConstraint _parentConstraint;
    private bool _isParentConstraintSet;

    private int _row;
    private int _column;


    protected override void ExecuteCustomActions(Collectible collectible, Action onCollectCommandExecuted)
    {
        collectible.Collider.enabled = false;

        if (CollectedCollectibles.Count == 0)
        {
            _parentConstraint = ParentTransform.gameObject.AddComponent<ParentConstraint>();
            ConstraintSource constraintSource = new ConstraintSource();
            constraintSource.sourceTransform = CharacterTransform;
            constraintSource.weight = 1;
            _parentConstraint.AddSource(constraintSource);
        }


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
        float currentTime = 0;

        var collectibleTransform = collectible.transform;
        collectibleTransform.parent = ParentTransform;

        Quaternion rotation = collectibleTransform.rotation;
        Vector3 position = collectibleTransform.position;


        while (currentTime < _lerpTime)
        {
            if (!_isParentConstraintSet && _parentConstraint != null)
            {
                _isParentConstraintSet = true;
                _parentConstraint.constraintActive = true;
                ParentTransform.position = Vector3.zero;
            }

            float step = currentTime / _lerpTime;


            Vector3 targetPosition = TargetTransforms[_column][_row].position - _distance;
            Quaternion targetRotation = TargetTransforms[_column][_row].rotation;

            collectibleTransform.position = Vector3.Lerp(position,
                targetPosition, step);

            collectibleTransform.rotation = Quaternion.Lerp(rotation,
                targetRotation, step);

            currentTime += Time.deltaTime;
            yield return null;
        }
    }
}