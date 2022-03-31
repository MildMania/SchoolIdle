using System;
using System.Collections.Generic;
using MMFramework.TasksV2;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private MMTaskExecutor _onCollidedTasks;

    [SerializeField] private GameObject _prefab;

    public Collider Collider;
    public bool IsCollided { get; private set; }
    public Action<Character> OnCollided;

    public bool TryCollide()
    {

        _onCollidedTasks?.Execute(this);

        OnCollided?.Invoke(this);

        return true;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            var ai = Instantiate(_prefab);

            ai.transform.position = transform.position;
            
        }
    }
}