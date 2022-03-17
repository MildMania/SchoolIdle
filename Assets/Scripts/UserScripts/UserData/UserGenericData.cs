using Boomlagoon.JSON;
using SerializableData;
using System;

public class UserGenericData : UserData
{
    public SerializableData<GenericData> Data { get; private set; }

    public class GenericData : ISerializableData
    {
        public string UserName { get; private set; }

        public GenericData()
        {

        }

        public GenericData(
            string username)
        {
            UserName = username;
        }

        private const string USER_NAME = "UserName";

        public JSONObject Serialize()
        {
            JSONObject obj = new JSONObject();

            obj.Add(USER_NAME, UserName);

            return obj;
        }

        public void Deserialize(JSONObject jsonObj)
        {
            if (jsonObj == null)
                return;

            UserName = jsonObj.GetString(USER_NAME);
        }
    }

    public UserGenericData(
        ISerializableDataIO<GenericData> dataIO)
    {
        Data = new SerializableData<GenericData>(
            new GenericData(),
            dataIO);
    }

    public UserGenericData(
        GenericData data,
        ISerializableDataIO<GenericData> dataIO)

    {
        Data = new SerializableData<GenericData>(
            data,
            dataIO);
    }

    public override void LoadData(Action onLoadedCallback)
    {
        Data.LoadData(onLoadedCallback);
    }

    public override void SaveData(Action onSavedCallback)
    {
        Data.SaveData(onSavedCallback);
    }
}

