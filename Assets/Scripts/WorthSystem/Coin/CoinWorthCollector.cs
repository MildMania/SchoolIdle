using System;
using MMFramework.UserSystem;
using UnityEngine;

[RequireComponent(typeof(IUserProvider))]
public class CoinWorthCollector : WorthCollector<CoinWorth>
{
    private CoinController _coinController;

    private void Awake()
    {
        _coinController = GetComponent<CoinController>();
    }

    private IUserProvider _userProvider;
    public IUserProvider UserProvider
    {
        get
        {
            if (_userProvider == null)
                _userProvider = GetComponent<IUserProvider>();

            return _userProvider;
        }
    }

    private CoinInventory _coinInventory;
    private CoinInventory _CoinInventory
    {
        get
        {
            if (_coinInventory == null)
            {
                UserProvider.User.InventoryController.TryGetInventory(out _coinInventory);
            }
   

            return _coinInventory;
        }
    }
    
    protected override void CollectWorthCustomActions(CoinWorth coinWorth)
    {
        Debug.Log("Coin Count : " + coinWorth.Count + " CoinType : " + coinWorth.Coin);
        Debug.Log("CollectWorthCustomActions");
        
   
        CoinTrackData coinTrackData =
            new CoinTrackData(
                coinWorth.Coin,
                count: coinWorth.Count);
        
        _CoinInventory.Tracker.TryAddToTracker(
            coinTrackData);
        
        _coinController.Collect();
        base.CollectWorthCustomActions(coinWorth);
    }
}