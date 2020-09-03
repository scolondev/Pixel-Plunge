using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementHandler : MonoBehaviour
{
    //All Achievements in the game
    public static AchievementHandler instance;
    string filename = "achievements.json";
    string path;

    public AchievementData achievementData;
    //Need to Save Worlds


    
    public void Awake()
    {
        instance = this;
        path = Application.persistentDataPath + "/" + filename;
        ReadData();
    }

    public void CompleteAchievement(Achievement achievement){
        int index = achievementData.achievements.IndexOf(achievement);
        achievementData.achievements[index].completed = true;
        SaveData();
    }

    public void SaveData()
    {
        AchievementWrapper wrapper = new AchievementWrapper();
        wrapper.achievementData = achievementData;

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
                AchievementWrapper wrapper = JsonUtility.FromJson<AchievementWrapper>(contents);
                achievementData = wrapper.achievementData;
            }
            else
            {
                Debug.Log("Unable to read the save data, file does not exist... creating new data");
                
                //If there's no data then we need to create some new data.
                //If there's no achievements already premade, we create some empty data.
                //This should not happen as we can set up the default instance of achievements in the inspector.
                if(achievementData.achievements.Count < 1){
                    achievementData = new AchievementData();
                }

                SaveData();
            }
        }
        catch (System.Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
}
