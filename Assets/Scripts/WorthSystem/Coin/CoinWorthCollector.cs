using UnityEngine;

public class CoinWorthCollector : WorthCollector<CoinWorth>
{
    [SerializeField] private CoinController _coinController;

    protected override void CollectWorthCustomActions(CoinWorth coinWorth)
    {
        _coinController.Collect();
        base.CollectWorthCustomActions(coinWorth);
    }
}