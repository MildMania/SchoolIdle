using System.Collections.Generic;
using UnityEngine;

public class CoinDropProvider : DropProvider
{
    [SerializeField] private Drop _coinDrop = null;
    [SerializeField] private int _coinCount = 0;

    public override List<DropData> GetDrops()
    {
        List<DropData> drops = new List<DropData>();

        for (int i = 0; i < _coinCount; i++)
            drops.Add(new CoinDropData(_coinDrop,ECoinType.Gold,1));

        return drops;
    }
}