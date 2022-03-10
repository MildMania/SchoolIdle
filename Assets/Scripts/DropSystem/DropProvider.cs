using System.Collections.Generic;
using UnityEngine;

public abstract class DropProvider : MonoBehaviour
{
    public abstract List<DropData> GetDrops();
}
