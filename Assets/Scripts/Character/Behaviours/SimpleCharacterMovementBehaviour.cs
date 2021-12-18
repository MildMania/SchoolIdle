using UnityEngine;

public class SimpleCharacterMovementBehaviour : BaseCharacterMovementBehaviour
{
    [SerializeField] private CharacterController _characterController = null;
    [SerializeField] private Transform _characterTransform;
    [SerializeField] private float _zSpeed = 2.0f;
    [SerializeField] private float _xSpeed = 5f;
    [SerializeField] private float _ySpeed = -1f;

    public override void Move(float xSwipeAmount)
    {
        var characterPosition = _characterTransform.position;
        Vector3 sideWayDir = _characterTransform.right * (xSwipeAmount - characterPosition.x);

        Vector3 direction = _characterTransform.forward * _zSpeed +
                            sideWayDir * _xSpeed +
                            new Vector3(0, _ySpeed, 0) * Time.deltaTime;

        _characterController.SimpleMove(direction);

        _characterController.Move(new Vector3(0, direction.y, 0));
    }
}