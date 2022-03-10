using System;
using UnityEngine;

public interface IMovementCommander
{
    Action<Vector3> OnMoveCommand { get; set; }
    Action OnMoveCancelledCommand { get; set; }
    Action<Vector3> OnLookAtCommand { get; set; }
}
