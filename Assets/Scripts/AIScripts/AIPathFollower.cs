using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class AIPathFollower : MonoBehaviour
{
    [SerializeField] private Transform _destination;

    [SerializeField] private Seeker _seeker;
    [SerializeField] private AIPath _aIPath;

    private bool _isActive = false;


    private void Start()
    {
        var path = _seeker.StartPath(transform.position, _destination.transform.position);
        _aIPath.SetPath(path);
    }

}
