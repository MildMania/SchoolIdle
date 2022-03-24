using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Pathfinding;

public class AIHelperAIPath : AIPath
{
    public Action OnPathCompleted;

    public override void OnTargetReached()
    {
        base.OnTargetReached();

        OnPathCompleted?.Invoke();
    }
}
