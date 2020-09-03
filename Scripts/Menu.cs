using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    //Preview your customization
    public Image preview;
    public Image preview_eye;
    public Sprite[] eyes;

    //Load a scene
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    //Setting your preference
    public void SetPersonalEye(int index)
    {
        preview_eye.sprite = eyes[index];

        PlayerHandler.instance.playerData.personalEyes = index;
        PlayerHandler.instance.SaveData();
    }
    //Setting your preference
    public void SetPersonalColor()
    {
        Color color = GetComponent<Image>().color;

        PlayerHandler.instance.playerData.personalColor = color;
        PlayerHandler.instance.SaveData();

        preview.color = color;
    }
}
