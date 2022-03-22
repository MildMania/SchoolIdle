using MMFramework.MMUI;
using TMPro;
using UnityEngine;
using UnityWeld.Binding;

public class UnlockableTextWidget : WidgetBase
{
	[SerializeField] private UnlockableObject _unlockableObject;
	
	[SerializeField] private TMP_Text _targetText;
	
	[SerializeField] private string _preFix = "$";
	
	protected override void AwakeCustomActions()
	{
		_unlockableObject.OnUnlockableInit += OnUnlockableInit;
		_unlockableObject.OnTryUnlock += OnTryUnlock;

		base.AwakeCustomActions();
	}

	protected override void OnDestroyCustomActions()
	{
		_unlockableObject.OnUnlockableInit -= OnUnlockableInit;
		_unlockableObject.OnTryUnlock -= OnTryUnlock;
		
		base.OnDestroyCustomActions();
	}

	private void OnTryUnlock(UnlockableTrackData unlockableTrackData)
	{
		int totalRequirement = _unlockableObject.Unlockable.GetRequirementCoin();
		_targetText.text = _preFix + (totalRequirement - unlockableTrackData.CurrentCount);
	}

	private void OnUnlockableInit(UnlockableTrackData unlockableTrackData)
	{
		int totalRequirement = _unlockableObject.Unlockable.GetRequirementCoin();
		_targetText.text = _preFix + (totalRequirement - unlockableTrackData.CurrentCount);
	}
}