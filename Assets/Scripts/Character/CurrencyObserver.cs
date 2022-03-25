using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyObserver : MonoBehaviour
{
    public Action<int> OnCurrencyUpdated { get; set; }
}
