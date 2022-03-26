using System;
using UnityEngine;

public interface IResource
{
    public Action<IResource> OnMoveRoutineFinished { get; set; }

    public void Move(Transform target, Transform container);
}