using System;
using System.Collections.Generic;
using MMFramework.TasksV2;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private MMTaskExecutor _onCollidedTasks;

    public Collider Collider;
    public bool IsCollided { get; private set; }
    public Action<Character> OnCollided;

    public bool TryCollide()
    {

        _onCollidedTasks?.Execute(this);

        OnCollided?.Invoke(this);

        return true;
    }

    private void Start()
    {
        Debug.Log(Application.persistentDataPath);
    }

}