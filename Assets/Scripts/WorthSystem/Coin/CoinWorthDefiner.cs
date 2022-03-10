using System.Collections.Generic;

public class CoinWorthDefiner : WorthDefinerWrapper
{
    public override List<IWorth> GetWorths()
    {
        return new List<IWorth>
        {
            new CoinWorth()
        };
    }
}