using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private List<Collectible> _collectedCollectibles = new List<Collectible>();
    private List<Collectible> _collidedCollectibles = new List<Collectible>();

    public List<Collectible> CollectedCollectibles
    {
        get => _collectedCollectibles;
        set => _collectedCollectibles = value;
    }

    public List<Collectible> CollidedCollectibles
    {
        get => _collidedCollectibles;
        set => _collidedCollectibles = value;
    }
}