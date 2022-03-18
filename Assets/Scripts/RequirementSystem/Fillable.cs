public class Fillable
{
	public int CurrentValue;

	public int TargetValue;

	public void Fill(int amount, out int fillAmount)
	{
		CurrentValue += amount;
		fillAmount = amount;
	}
}