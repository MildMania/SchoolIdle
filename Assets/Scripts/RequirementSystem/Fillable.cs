public class Fillable
{
	public int CurrentValue;

	public void Fill(int amount, out int fillAmount)
	{
		CurrentValue += amount;
		fillAmount = amount;
	}
	public Fillable(
		int currentValue)
	{
		CurrentValue = currentValue;
	}
}