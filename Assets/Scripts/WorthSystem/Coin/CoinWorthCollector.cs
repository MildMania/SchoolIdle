using UnityEngine;

public class CoinWorthCollector : WorthCollector<CoinWorth>
{
    [SerializeField] private CoinController _coinController;

    protected override void CollectWorthCustomActions(CoinWorth coinWorth)
    {
        Debug.Log("Coin Count : " + coinWorth.Count + " CoinType : " + coinWorth.CoinType);
        Debug.Log("CollectWorthCustomActions");
        _coinController.Collect();
        base.CollectWorthCustomActions(coinWorth);
    }
}