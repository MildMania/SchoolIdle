using System;
using System.Collections.Generic;

public class UserDataComposition : UserData
{
    private Dictionary<Type, UserData> _userDataMap;

    public UserDataComposition(params UserData[] userDatas)
    {
        InitUserDataMap(userDatas);
    }

    public sealed override void InitUser(User user)
    {
        foreach (var kvp in _userDataMap)
            kvp.Value.InitUser(user);

        base.InitUser(user);
    }

    private void InitUserDataMap(UserData[] userDatas)
    {
        _userDataMap = new Dictionary<Type, UserData>();

        foreach (UserData userData in userDatas)
            _userDataMap.Add(userData.GetType(), userData);
    }
    public override void LoadData(Action onLoadedCallback)
    {
        int subDataCount = _userDataMap.Count;
        int loadedSubDataCount = 0;

        void onSubDataLoaded()
        {
            loadedSubDataCount++;

            if (loadedSubDataCount < subDataCount)
                return;

            onLoadedCallback?.Invoke();

            DataLoaded();
        }

        foreach (var kvp in _userDataMap)
            kvp.Value.LoadData(onSubDataLoaded);
    }

    public override void SaveData(Action onSavedCallback)
    {
        int subDataCount = _userDataMap.Count;
        int savedSubDataCount = 0;

        void onSubDataSaved()
        {
            savedSubDataCount++;

            if (savedSubDataCount < subDataCount)
                return;

            onSavedCallback?.Invoke();
        }

        foreach (var kvp in _userDataMap)
            kvp.Value.SaveData(onSubDataSaved);
    }

}
