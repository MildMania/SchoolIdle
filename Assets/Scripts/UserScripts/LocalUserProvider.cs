using UnityEngine;

public class LocalUserProvider : MonoBehaviour,
    IUserProvider
{
    public User User
    {
        get
        {
            return UserManager.Instance.LocalUser;
        }
    }
}
