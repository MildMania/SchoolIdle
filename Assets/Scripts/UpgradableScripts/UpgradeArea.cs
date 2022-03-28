using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeArea : MonoBehaviour
{
    [SerializeField] private GameObject _widgetObject;
    
    public void ShowWidget()
    {
        _widgetObject.SetActive(true);
    }

    public void HideWidget()
    {
        _widgetObject.SetActive(false);
    }
}
