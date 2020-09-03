using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    //Everything about the player that needs to be stored
    public float coinCount = 0;
    public float essenceCount = 0;
    public Color personalColor = new Color(0, 0, 0, 1f);
    public int personalEyes = 0;
}
