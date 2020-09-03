using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandler : MonoBehaviour
{
 
    public static PlayerHandler instance;
    string filename = "player.json";
    string path;

   //All stock in the game
    public PlayerData playerData;
    
    public void Awake()
    {
        instance = this;
        path = Application.persistentDataPath + "/" + filename;
        ReadData();
    }

    public void SaveData()
    {
        PlayerWrapper wrapper = new PlayerWrapper();
        wrapper.playerData = playerData;

        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(path, contents);
    }

    public void ReadData()
    {
        try
        {
            if (System.IO.File.Exists(path))
            {

                string contents = System.IO.File.ReadAllText(path);
                PlayerWrapper wrapper = JsonUtility.FromJson<PlayerWrapper>(contents);
                playerData = wrapper.playerData;
            }
            else
            {
                Debug.Log("Unable to read the save data, file does not exist... creating new data");
                
                //If there's no data then we need to create some new data.
                //If there's no stock already premade, we create some empty data.
                SaveData();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
