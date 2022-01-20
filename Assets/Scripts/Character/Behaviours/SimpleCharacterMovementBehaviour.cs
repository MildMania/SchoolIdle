using UnityEngine;

public class SimpleCharacterMovementBehaviour : BaseCharacterMovementBehaviour
{
    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private float _zSpeed = 2.0f;
    [SerializeField] private float _xSpeed = 5f;
    [SerializeField] private float _ySpeed = -1f;

    public override void MoveCustomActions(float xSwipeAmount)
    {
        var characterPosition = _characterTransform.position;
        Vector3 sideWayDir = _characterTransform.right * (xSwipeAmount - characterPosition.x);

        Vector3 direction = _characterTransform.forward * _zSpeed +
                            sideWayDir * _xSpeed +
                            new Vector3(0, _ySpeed, 0);

        _characterController.Move(direction * Time.deltaTime);
    }

    private void LateUpdate()
    {
        KeepOnPlatform();
    }

    private void KeepOnPlatform()
    {
        var characterPosition = _characterTransform.position;

        if (characterPosition.x < LevelBoundaryProvider.Instance.GetLeftBoundary().x)
        {
            _characterTransform.position = new Vector3(LevelBoundaryProvider.Instance.GetLeftBoundary().x,
                _characterTransform.position.y, characterPosition.z);
        }
        else if (characterPosition.x > LevelBoundaryProvider.Instance.GetRightBoundary().x)
        {
            _characterTransform.position = new Vector3(LevelBoundaryProvider.Instance.GetRightBoundary().x,
                _characterTransform.position.y, characterPosition.z);
        }
    }
}