using UnityEngine;

public abstract class BaseCharacterMovementBehaviour : MonoBehaviour
{
    [SerializeField] private BaseCharacterRotationBehaviour _characterRotationBehaviour;

    public void Move(float xSwipeAmount)
    {
        if (_characterRotationBehaviour != null)
        {
            _characterRotationBehaviour.Rotate(xSwipeAmount);
        }

        MoveCustomActions(xSwipeAmount);
    }

    public abstract void MoveCustomActions(float xSwipeAmount);
}