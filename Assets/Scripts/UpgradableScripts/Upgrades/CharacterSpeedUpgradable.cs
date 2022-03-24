using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpeedUpgradable : Upgradable
{
    private void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.E))
        {

            if (_upgradableTrackData.Level > RequirementData.Length - 1)
            {
                return;
            }
            
            if (TryUpgrade(UserManager.Instance.LocalUser, RequirementData[_upgradableTrackData.Level]))
            {
                
                Debug.Log("UPGRADE FAILED");
                
                var requirementCoin = (RequirementCoin) RequirementData[_upgradableTrackData.Level];
                int totalRequiredAmount = requirementCoin.RequirementData.RequiredAmount;
				
                UpgradableTrackData upgradableTrackData = new UpgradableTrackData(_upgradableTrackData.TrackID, 
                    ++_upgradableTrackData.Level);
				
                var userUpgradableData = UserManager.Instance.LocalUser.GetUserData<UserUpgradableData>();
                userUpgradableData.Tracker.TryUpsert(upgradableTrackData);

                var userCoinInventoryData = UserManager.Instance.LocalUser.GetUserData<UserCoinInventoryData>();
                Coin trackableCoin;
                userCoinInventoryData.Tracker.TryGetSingle(ECoin.Gold, out trackableCoin);
				
                CoinTrackData coinTrackData =
                    new CoinTrackData(
                        ECoin.Gold,
                        count: trackableCoin.TrackData.CurrentCount - totalRequiredAmount);
		
                trackableCoin.UpdateData(coinTrackData);
                UserManager.Instance.LocalUser.SaveData(OnSaved);

                void OnSaved()
                {
                    Debug.Log("upgrade saved");
                }
				
                Debug.Log("UPGRADE YAPILDI.");
				
                OnUpgraded?.Invoke(_upgradableTrackData);
            };
        }
    }
}
