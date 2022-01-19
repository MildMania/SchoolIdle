using PathCreation;
using UnityEngine;

public class SplineCharacterMovementBehaviour : BaseCharacterMovementBehaviour
{
    [SerializeField] private FormationController _formationController;
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private float _speed = 5.0f;
    [SerializeField] private Transform _parentTransform;
    public float DistanceTravelled { get; private set; }

    public PathCreator PathCreator
    {
        get => _pathCreator;
    }

    public override void MoveCustomActions(float xSwipeAmount)
    {
        var localPosition = _characterTransform.localPosition;
        var characterPosition = localPosition;


        DistanceTravelled += _speed * Time.deltaTime;
        _parentTransform.position = PathCreator.path.GetPointAtDistance(DistanceTravelled) + Vector3.up;

        _parentTransform.rotation =
            Quaternion.Euler(0, PathCreator.path.GetRotationAtDistance(DistanceTravelled).eulerAngles.y, 0);


        localPosition += new Vector3(xSwipeAmount - characterPosition.x, 0, 0);
        _characterTransform.localPosition = localPosition;
        foreach (var leadingTransforms in _formationController.TargetTransforms)
        {
            leadingTransforms[0].localPosition += new Vector3(xSwipeAmount - characterPosition.x, 0, 0);
        }
    }
}