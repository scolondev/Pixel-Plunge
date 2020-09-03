using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcadeData : MonoBehaviour
{
    public static ArcadeData instance;
    string filename = "arcade.json";
    string path;

    public WorldData worldData;
    //Need to Save Worlds


    // Start is called before the first frame update
    public void Awake()
    {
        instance = this;
        path = Application.persistentDataPath + "/" + filename;
        ReadData();
    }

    public void SaveData()
    {
        ArcadeWrapper wrapper = new ArcadeWrapper();
        wrapper.worldData = worldData;

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
                ArcadeWrapper wrapper = JsonUtility.FromJson<ArcadeWrapper>(contents);
                worldData = wrapper.worldData;
            }
            else
            {
                Debug.Log("Unable to read the save data, file does notexist");
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
