using System.Collections.Generic;
using UnityEngine;

public abstract class BaseResourceProvider<TResource> : MonoBehaviour, IResourceProvider where TResource : IResource
{
    public List<TResource> Resources { get; set; } = new List<TResource>();
}