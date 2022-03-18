using UnityEngine;

public class PlayerUserProvider : MonoBehaviour,
    IUserProvider
{
    [SerializeField] private PlayerWrapper _playerWrapper = null;
    public User User
    {
        get
        {
            return _playerWrapper.Player.User;
        }
    }
}
