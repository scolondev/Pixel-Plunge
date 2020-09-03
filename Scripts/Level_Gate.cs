using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Gate : MonoBehaviour
{
    public List<bool> doors = new List<bool>();
    public void UnlockRequirement()
    {
        for(int i = 0; i < doors.Count; i++)
        {
            if(doors[i] == false)
            {
                doors[i] = true;
                CheckGates();
                return;
            }
        }
    }

    public void CheckGates()
    {
        if (!doors.Contains(false))
        {
            Open();
        }
    }
    public void Open()
    {
        this.gameObject.SetActive(false);
    }
}
