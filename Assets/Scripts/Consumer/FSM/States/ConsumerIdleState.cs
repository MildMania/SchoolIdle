using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EState = ConsumerFSMController.EState;


public class ConsumerIdleState : State<EState, EState>
{
    protected override EState GetStateID()
    {
        return EState.Idle;
    }
}
