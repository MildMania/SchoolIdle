using System.Collections;
using System.Collections.Generic;
using MMFramework.TasksV2;
using UnityEngine;

public class Task_OnUpgraded : MMTask
{

    [SerializeField] private CoinController _coinController;

    public override ETaskStatus Execute(MonoBehaviour caller)
    {
        _coinController.UpdateCoinCount();

        return ETaskStatus.Completed;
    }
}