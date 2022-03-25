using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class AIMovementBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _rootTransform;

    [SerializeField] private Seeker _seeker;
    [SerializeField] private AIHelperAIPath _aIPath;

    private float _movementSpeed = 1;
    public float MovementSpeed => _movementSpeed;

    private System.Action _currentPathCompletedCallback;

    private void Awake()
    {
        _aIPath.OnPathCompleted += OnPathCompleted;
    }

    private IEnumerator MoveRoutine(Vector3 targetPos)
    {
        var path = _seeker.StartPath(_rootTransform.position, targetPos);
        yield return StartCoroutine(path.WaitForPath());
        _aIPath.SetPath(path);
    }

    public void MoveDestination(Vector3 targetPos, System.Action onPathCompletedCallback = null)
    {
        _currentPathCompletedCallback = onPathCompletedCallback;
        StartCoroutine(MoveRoutine(targetPos));
    }

    public void Stop()
    {
        _movementSpeed = 0;
    }

    private void OnPathCompleted()
    {
        if(_currentPathCompletedCallback != null)
            _currentPathCompletedCallback();
    }
}
