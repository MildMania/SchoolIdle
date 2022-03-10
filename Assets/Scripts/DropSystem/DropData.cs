using UnityEngine;

[System.Serializable]
public class DropData
{
    [SerializeField] private Drop _drop = null;
    public Drop Drop => _drop;

    public DropData()
    {

    }

    public DropData(Drop drop)
    {
        _drop = drop;
    }
}
