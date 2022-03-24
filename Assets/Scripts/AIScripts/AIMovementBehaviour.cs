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

    private IEnumerator MoveRoutine(Vector3 targetPos, System.Action onPathCompleted)
    {
        var path = _seeker.StartPath(_rootTransform.position, targetPos);
        yield return StartCoroutine(path.WaitForPath());
        _aIPath.OnPathCompleted += onPathCompleted;
        _aIPath.SetPath(path);
    }

    public void MoveDestination(Vector3 targetPos, System.Action onPathCompleted)
    {
        StartCoroutine(MoveRoutine(targetPos, onPathCompleted));
    }

    public void Stop()
    {
        _movementSpeed = 0;
    }
}
