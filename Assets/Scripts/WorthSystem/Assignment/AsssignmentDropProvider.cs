using System.Collections.Generic;
using UnityEngine;

public class AsssignmentDropProvider : DropProvider
{
    [SerializeField] private Drop _assignmentDrop = null;
    [SerializeField] private int _assignmentCount = 0;

    public override List<DropData> GetDrops()
    {
        List<DropData> drops = new List<DropData>();

        for (int i = 0; i < _assignmentCount; i++)
            drops.Add(new AssignmentDropData(_assignmentDrop));

        return drops;
    }
}