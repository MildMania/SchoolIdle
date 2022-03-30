using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInteraction : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;

    public Vector3 GetInteractionPoint()
    {
        return _interactionPoint.position;
    }
}
