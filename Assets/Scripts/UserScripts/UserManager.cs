using UnityEngine;

public class UserManager : MonoBehaviour
{
    private static UserManager _instance;
    public static UserManager Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<UserManager>();

            return _instance;
        }
    }

    private User _localUser;
    public User LocalUser
    {
        get
        {
            if (_localUser == null)
                CreateUser();

            return _localUser;
        }
    }

    private void Awake()
    {
        if (_localUser == null)
            CreateUser();
    }

    private void CreateUser()
    {
        _localUser = UserFactory.CreateUser();
        
        LocalUser.LoadData(null);
    }


    public int GetCoinCount(ECoin coinType)
    {
        Coin trackable;
		
        _localUser.GetUserData<UserCoinInventoryData>().Tracker.TryGetSingleTrackable(coinType,out trackable);

        int currentCount = trackable.TrackData.CurrentCount;
		
        Debug.Log("CURRENT GOLD COUNT" + currentCount);

        return currentCount;
    }
}
