using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[CreateAssetMenu(fileName = "RigidStoreCommand", menuName = "ScriptableObjects/Storable/Store/RigidStoreCommand",
    order = 1)]
public class RigidStoreCommand : StoreCommandBase
{
    [SerializeField] private float _lerpTime = 0.25f;
    [SerializeField] private Vector3 _distance;

    private ParentConstraint _parentConstraint;
    private bool _isParentConstraintSet;

    private int _row;
    private int _column;
    
    protected override void ExecuteCustomActions(StorableBase storable, Action onStoreCommandExecuted)
    {
        storable.transform.SetParent(ParentTransform);
        
        _row = StorableList.Count / TargetTransforms.Length;
        _column = StorableList.Count % TargetTransforms.Length;

        TargetTransforms[_column].Add(storable.transform);
        StorableList.Add(storable);

        storable.MoveRoutine = MoveRoutine(storable);
        CoroutineRunner.Instance.StartCoroutine(storable.MoveRoutine);

        onStoreCommandExecuted?.Invoke();

    }
    
    private IEnumerator MoveRoutine(StorableBase storable)
    {
        float currentTime = 0;

        var storableTransform = storable.transform;

        Quaternion rotation = storableTransform.rotation;
        Vector3 position = storableTransform.position;

        
        while (currentTime < _lerpTime)
        {
            float step = currentTime / _lerpTime;


            Vector3 targetPosition = TargetTransforms[_column][_row].position - _distance;
            Quaternion targetRotation = TargetTransforms[_column][_row].rotation;

            storableTransform.position = Vector3.Lerp(position,
                targetPosition, step);

            storableTransform.rotation = Quaternion.Lerp(rotation,
                targetRotation, step);

            currentTime += Time.deltaTime;
            yield return null;
        }
        
        storableTransform.position = TargetTransforms[_column][_row].position - _distance;
        storableTransform.rotation = TargetTransforms[_column][_row].rotation;
        
        
            
            
    
    }

}
