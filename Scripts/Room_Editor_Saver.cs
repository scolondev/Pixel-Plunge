using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room_Editor_Saver : MonoBehaviour
{
    public static Room_Editor_Saver instance;
    string filename = "room.json";
    string backup = "room_backup.json";
    string path;

    public Room_Editor_Data editorData;
    //Need to Save Worlds


    // Start is called before the first frame update
    public void Awake()
    {
        instance = this;
        path = Application.persistentDataPath + "/" + filename;
        backup = Application.persistentDataPath + "/" + filename;
        ReadData();
    }

    public void Update()
    {
        //Save another copy of this when needed.
        if (Input.GetKeyDown(KeyCode.B)){
            BackupData();
        }
    }
    public void SaveData()
    {
        Room_Editor_Wrapper wrapper = new Room_Editor_Wrapper();
        wrapper.data = editorData;

        string contents = JsonUtility.ToJson(wrapper, true);
        System.IO.File.WriteAllText(path, contents);

        System.IO.File.WriteAllText(backup, contents);
    }

    public void BackupData()
    {

        Room_Editor_Wrapper wrapper = new Room_Editor_Wrapper();
        wrapper.data = editorData;

        string contents = JsonUtility.ToJson(wrapper, true);

        System.IO.File.WriteAllText(backup, contents);
    }

    public void ReadData()
    {
        try
        {
            if (System.IO.File.Exists(path))
            {         
                string contents = System.IO.File.ReadAllText(path);
                Room_Editor_Wrapper wrapper = JsonUtility.FromJson<Room_Editor_Wrapper>(contents);
                editorData = wrapper.data;
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
