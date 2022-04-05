using System;
using System.Collections.Generic;
using UnityEngine;

public class Deliverer : MonoBehaviour
{
    [SerializeField] private UpdatedFormationController _updatedFormationController;
    [SerializeField] public Transform Container;

    [SerializeField] public Transform MoneyContainer;

    public Action<bool> OnContainerEmpty; 

    public UpdatedFormationController FormationController
    {
        get => _updatedFormationController;
        set => _updatedFormationController = value;
    }

    public List<IResource> Resources { get; set; } = new List<IResource>();
    public List<IResource> MoneyResources { get; set; } = new List<IResource>();

    public int GetLastResourceIndex<TResource>()
    {
        for (int i = Resources.Count - 1; i >= 0; i--)
        {
            if (Resources[i] is TResource)
            {
                return i;
            }
        }

        return -1;
    }
    
    public int GetLastMoneyResourceIndex<TResource>()
    {
        for (int i = MoneyResources.Count - 1; i >= 0; i--)
        {
            if (MoneyResources[i] is TResource)
            {
                return i;
            }
        }

        return -1;
    }
}