using MMFramework.TasksV2;
using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DropProvider))]
public class Dropper : MonoBehaviour
{
    private DropProvider _dropProvider;
    public DropProvider DropProvider
    {
        get
        {
            if (_dropProvider == null)
                _dropProvider = GetComponent<DropProvider>();

            return _dropProvider;
        }
    }

    [SerializeField] private bool _dropOnAwake = false;

    [SerializeField]
    private MMTaskExecutor _dropperTaskExecutor =
        new MMTaskExecutor();

    [SerializeField] private bool _keepAsParent = false;

    public Drop NextDrop { get; private set; }

    public Action<Dropper, List<Drop>> OnDropping { get; set; }

    private void Awake()
    {
        if (_dropOnAwake)
            Drop();
    }

    public void Drop()
    {
        List<DropData> dropInfos = DropProvider.GetDrops();
        List<Drop> cloneDrops = CloneDrops(dropInfos);

        OnDropping?.Invoke(
            this,
            cloneDrops);

        for (int i = 0; i < cloneDrops.Count; i++)
        {
            NextDrop = cloneDrops[i];
            SetPosition(NextDrop);

            _dropperTaskExecutor.Execute(this);

            NextDrop.Invoke(this, dropInfos[i]);
        }
    }

    private void SetPosition(Drop drop)
    {
        drop.transform.position = transform.position;
    }

    private List<Drop> CloneDrops(List<DropData> dropInfos)
    {
        List<Drop> cloneDrops = new List<Drop>();

        foreach (DropData dropInfo in dropInfos)
            cloneDrops.Add(CloneDrop(dropInfo.Drop));

        return cloneDrops;
    }

    private Drop CloneDrop(Drop drop)
    {
        Drop cloneDrop = drop.Clone();

        if (_keepAsParent)
            cloneDrop.transform.SetParent(transform);

        return cloneDrop;
    }    
}
