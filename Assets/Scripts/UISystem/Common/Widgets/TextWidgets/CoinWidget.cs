using TMPro;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class CoinWidget : TextWidget
{
	[SerializeField] private TMP_Text _coinText;
	[SerializeField] private CurrencyObserver _currencyObserver;
	
	protected override void AwakeCustomActions()
	{
		base.AwakeCustomActions();

		_currencyObserver.OnCurrencyUpdated += OnCurrencyUpdated;
	}

	private void OnCurrencyUpdated(int currencyCount)
	{
		_coinText.text = currencyCount.ToString();
	}

	protected override void OnDestroyCustomActions()
	{
		base.OnDestroyCustomActions();
		
		_currencyObserver.OnCurrencyUpdated -= OnCurrencyUpdated;
	}

}