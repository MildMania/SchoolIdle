using TMPro;
using UnityEngine;
using UnityWeld.Binding;

[Binding]
public class CoinWidget : TextWidget
{
	[SerializeField] private CoinController _coinController;
	
	[SerializeField] private TMP_Text _coinText;
	
	protected override void AwakeCustomActions()
	{
		_coinController.OnCoinUpdated += OnCoinUpdated;
		_coinController.OnCoinInit += OnCoinInit;
		base.AwakeCustomActions();
	}

	protected override void OnDestroyCustomActions()
	{
		_coinController.OnCoinUpdated -= OnCoinUpdated;
		_coinController.OnCoinInit -= OnCoinInit;
		base.OnDestroyCustomActions();
	}
	
	private void OnCoinInit(int totalCoin)
	{
		_coinText.text = totalCoin.ToString();
	}

	private void OnCoinUpdated(int totalCoin)
	{
		_coinText.text = totalCoin.ToString();
	}
}