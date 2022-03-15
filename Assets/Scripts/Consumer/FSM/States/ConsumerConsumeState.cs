using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EState = ConsumerFSMController.EState;


public class ConsumerConsumeState : State<EState, EState>
{
    protected override EState GetStateID()
    {
        return EState.Consume;
    }
}