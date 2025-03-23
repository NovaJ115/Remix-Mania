using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    
    public static void SavePlayer(PlayerMovementV2 player)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        PlayerData data = new PlayerData(player);
        
        formatter.Serialize(stream, data);
        stream.Close();

    }

    public static void SaveTimer(InitiateRemixManager remixManager)
    {

        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/timer.save";
        FileStream stream = new FileStream(path, FileMode.Create);

        TimerData data = new TimerData(remixManager);

        formatter.Serialize(stream, data);
        stream.Close();

    }


    public static PlayerData LoadPlayer()
    {

        string path = Application.persistentDataPath + "/player.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            PlayerData data = formatter.Deserialize(stream) as PlayerData;
            stream.Close();


            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }
    public static TimerData LoadTimer()
    {

        string path = Application.persistentDataPath + "/timer.save";
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);

            TimerData data = formatter.Deserialize(stream) as TimerData;
            stream.Close();


            return data;
        }
        else
        {
            Debug.LogError("Save file not found in " + path);
            return null;
        }

    }


}
