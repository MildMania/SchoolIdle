using System.Collections.Generic;

public class AssignmentWorthDefiner : WorthDefinerWrapper
{
    public override List<IWorth> GetWorths()
    {
        return new List<IWorth>
        {
            new AssignmentWorth()
        };
    }
}