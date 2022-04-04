using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using MMFramework.Utilities;

public class AIInteraction : MonoBehaviour
{
    [SerializeField] private Transform _rotationTarget;
    public Transform RotationTarget => _rotationTarget;

    [SerializeField] private Collider _collider;

    public Vector3 GetInteractionPoint()
    {
        return Utilities.RandomPointInBounds(_collider.bounds);
    }


}
