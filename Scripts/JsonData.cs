using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ***THIS SCRIPT IS A TEST SCRIPT AND IS NOT USED FOR ANY FUNCTION WITHIN THE GAME
/// </summary>
public class JsonData : MonoBehaviour
{
    string filename = "placeholder.json";
    string path;

    

    // Start is called before the first frame update
    void Start()
    {
        path = Application.persistentDataPath + "/" + filename;
        Debug.Log(path);
    }

    // Update is called once per frame
    void Update()
    {
      /*  if (Input.GetKeyDown(KeyCode.S))
        {
            gameData.date = System.DateTime.Now.ToShortDateString();
            gameData.time = System.DateTime.Now.ToShortTimeString();
            SaveData();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReadData();
        } */
    }

    void SaveData(UnityEngine.Object saveData, string filename)
    {
        path = Application.persistentDataPath + "/" + filename;
        string contents = JsonUtility.ToJson(saveData, true);
        System.IO.File.WriteAllText(path, contents);
    }

    void ReadData(UnityEngine.Object saveData, string filename)
    {
        path = Application.persistentDataPath + "/" + filename;
        try
        {
            if (System.IO.File.Exists(path))
            {
                string contents = System.IO.File.ReadAllText(path);
              //  gameData = JsonUtility.FromJson<saveData>(contents);
            }
            else
            {
                Debug.Log("Unable to read the save data, file does not exist");
               // gameData = new GameData();
            }
        } catch(System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
