using UnityEngine;

public interface IMovementExecutor
{
    void Move(Vector3 direction, float speed);
    void Stop();
    void LookAt(Vector3 targetPos);
    
    float MovementSpeed { get; }
}
